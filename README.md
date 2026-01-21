# Proyecto Backend - OutletRentalCars

Web API escalable para la búsqueda y reserva de vehículos construida con .NET 8 y Clean Architecture. Implementa una solución híbrida de persistencia con MySQL (transaccional) y MongoDB (configuración), validación de reglas de negocio complejas y pruebas automatizadas (Unit e Integration).

---

Aplicación Backend para gestión de productos (prueba técnica).  
Incluye **backend en .NET 8.0**.

---

## 🚀 Tecnologías y Herramientas utilizadas

- C# - .Net Core - Framework .Net8
- VisualStudio 2022
- REST API (simulada o real)
---

## 📂 Estructura del proyecto

```text
/OutletRentalCars # API .NET (Clean Architecture)
	|- OutletRental.Application/ 
		|- Features/
			|- Bookings/
				|- Create/
			|- Vehicles/
				|- Search/
		|- Interfaces/
	|- OutletRental.Domain/
		|- Common/
		|- Entities/
		|- Events/
		|- ValueObjects/
	|- OutletRental.Infrastructure/
		|- Migrations/
		|- Persistence/
		|- Repositories/
	|- OutletRental.Tests/
		|- Integration/
	|- OutletRental.WebApi/
		|- Controllers/
		|- appsettings.json
	|- README.md
	|- docker-compose.yml
```
	
 ---

 ## ⚙️ Backend (.NET 8)

### 📌 Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

---

### Pasos:

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/edwsilme/OutletRentalCars.git
   ``` 

2. Cambiar a Rama develop:
   ```bash
   git switch develop 
   ```
   
3.  Configurar Connection String
- Abre OutletRental.WebApi/appsettings.json.
- En DefaultConnection, reemplaza el Server por el nombre de tu servidor (ej. Server=TU_PC_NOMBRE o Server=.).
   
   
---

## 🚀 Cómo ejecutar el proyecto

1. **Requisitos**: Tener Docker Desktop instalado (o instancias locales de MySQL y MongoDB).
2. **Configuración**: Revisar `appsettings.json` para las cadenas de conexión.
3. **Base de Datos**: 
   - El sistema usa **Data Seeding** automático. Al ejecutar la API por primera vez, se crearán las tablas en MySQL y las colecciones en MongoDB con datos de prueba (Toyota Corolla).
4. **Ejecución**:
   - Abrir solución en Visual Studio.
   - Seleccionar `OutletRental.WebApi` como proyecto de inicio.
   - Presionar F5. Swagger se abrirá en `https://localhost:7082/swagger`.
   
---

### 🐳 Ejecución rápida con Docker (Recomendado)

Si tienes Docker instalado, puedes levantar las bases de datos necesarias con un solo comando:

```bash
docker-compose up -d
```

Esto levantará:

- **MySQL**: puerto `3306`
- **MongoDB**: puerto `27017`

---

## 🛠 Justificación Técnica

### 1. Persistencia Políglota (MySQL + MongoDB)
Se optó por persistencia políglota para aprovechar las fortalezas de cada motor según el dominio:
- **MySQL (Relacional):** Para el núcleo de las transacciones (Vehículos, Localidades y Reservas). Esto garantiza la consistencia ACID que se requiere para evitar, por ejemplo, la doble reserva de un mismo vehículo haciendo uso de claves foráneas y restricciones de integridad.
- **MongoDB (NoSQL):** Para la Configuración de Mercados. La reglas de negocio que les dicen a los vehículos qué categorías se permiten en cada país son, por lo general, dinámicas y pueden tener diferentes estructuras. MongoDB da la flexibilidad de esquema suficiente para cambiar estas reglas sin necesidad de ejecutar migraciones complejas de base de datos.
### 2. Implementación de Clean Architecture
La solución se estructuró en capas para asegurar que la lógica de negocio sea independiente de los detalles de la infraestructura:
- **Domain:** Para las entidades puras como Vehicle y sus reglas de estado (VehicleStatus).
- **Application:** Para la orquestación de los casos de uso en Handlers (por ejemplo, SearchVehiclesHandler), orquestando la lógica sin llegar a saber si los datos provienen de MySQL o MongoDB.
- **Infrastuctura:** Implementación de repositorios y preparación de la configuración para el acceso a datos, donde se subsano el mapeo de los tipos Numéricos para los Enums y la conectividad correspondida para ambos motores.
### 3. Dificultades Técnicas superadas
Durante el desarrollo existen dificultades críticas de integración
- **Mapeo de tipos:** Se corrigieron las excepciones de conversión (InvalidCastException) garantizando que los Enums de C# eran almacenados como enteros (int) en el motor relacional obteniendo así una mejora en el tiempo de respuesta de las consultas.
- **Sincronización datos:** se implementó un DbInitializer que asegura que ambos motores dispongan de datos consistentes para las pruebas desde elprimer arranque (Seed Data).

---

## 📸 Vistas del Proyecto

:arrow_forward: Pantalla de inicio de API (Swagger)<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/001.png" width="500">

:arrow_forward: Ver Persistencia (MySQL)<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/002a.png" width="500">

:arrow_forward: Ver Persistencia (MongoDB)<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/002b.png" width="500">

:arrow_forward: Pantalla de ejecución Get Vehiculos<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/003a.png" width="500">

:arrow_forward: Pantalla resultado de ejecución Get Vehiculos:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/003b.png" width="500">

:arrow_forward: Ejecución de Pruebas unitarias y de INtegración<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-OutletRentalCars/004.png" width="500">
