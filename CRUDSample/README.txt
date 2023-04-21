# CRUD Ejemplo

Links:
CRUD Visual: https://localhost:7064/Productos
API Endpoints: https://localhost:7064/api/Producto/ (usar Postman)
    Protocoles: GET: Read, POST: Create, PUT: Update, DELETE: Delete

Nota: Compilar con la opcion de https (no docker)

Base de datos:
SQL Express 2017 instalada localmente. 
Nombre de la base de datos mcsVecinos
Ir a Tools -> Connect to database para hacer la conexion a la base de datos.

Tabla Productos:
CREATE TABLE [dbo].[Productos]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(300) NULL, 
    [Descripcion] NVARCHAR(MAX) NULL, 
    [Costo] NUMERIC(18, 2) NULL, 
    [Precio] NUMERIC(18, 2) NULL
)

Comando usado para generar el modelo:
1. Crear carpeta "Models"
2. Ir a Tools -> Nuget Package Manager -> Package Manager Console
3. Ejecutar:
Scaffold-DbContext "Server=localhost\SQLEXPRESS2017;Database=mcsVecinos;Integrated Security=True;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir  -ForceModels
4. Agregar en Program.cs la invocacion al mcsVecinosContext, justo despues de la linea builder.Services.AddRazorPages();
builder.Services.AddDbContext<CrudSample.Models.McsVecinosContext>(); 

