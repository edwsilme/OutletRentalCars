using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MySqlConnector;

namespace OutletRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DiagnosticController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var diagnostics = new
            {
                MySql = await TestMySql(),
                MongoDb = await TestMongo(),
                ServerTime = DateTime.Now
            };

            return Ok(diagnostics);
        }

        private async Task<string> TestMySql()
        {
            try
            {
                using var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
                await conn.OpenAsync();
                return "Connected Successfully";
            }
            catch (Exception ex) { return $"Error: {ex.Message}"; }
        }

        private async Task<string> TestMongo()
        {
            try
            {
                var client = new MongoClient(_config["MongoDbSettings:ConnectionString"]);
                using var cursor = await client.ListDatabaseNamesAsync();
                await cursor.ToListAsync();
                return "Connected Successfully";
            }
            catch (Exception ex) { return $"Error: {ex.Message}"; }
        }
    }
}
