using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OutletRental.Application.Features.Bookings.Create;
using OutletRental.Application.Features.Vehicles.Search;
using OutletRental.Application.Interfaces;
using OutletRental.Infrastructure.Persistence;
using OutletRental.Infrastructure.Repositories;
using ServerVersion = Microsoft.EntityFrameworkCore.ServerVersion;


var builder = WebApplication.CreateBuilder(args);

// DB Conections
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings");

// SERVICE RECORD(DI)
// Infrastructure: Databases
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoSettings["ConnectionString"]));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoSettings["DatabaseName"]));

// Infrastructure: Repositories
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IMarketRepository, MarketRepository>();

// Aplication: Handlers
builder.Services.AddScoped<SearchVehiclesHandler>();
builder.Services.AddScoped<CreateBookingHandler>();

// WebApi: Tools
builder.Services.AddControllers()
    .AddJsonOptions(options => {
         options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
     });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Data SEED
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try 
    {
        var mysqlContext = services.GetRequiredService<ApplicationDbContext>();
        var mongoDb = services.GetRequiredService<IMongoDatabase>();

        await DbInitializer.SeedData(mysqlContext, mongoDb);
    } 
    catch (Exception ex)
    {
        Console.WriteLine($"Error en Seed: {ex.Message}");
    }
   
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }