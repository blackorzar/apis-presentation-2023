using Microsoft.EntityFrameworkCore;
using CrudSample.Models;
using Microsoft.AspNetCore.OpenApi;
namespace CrudSample;

public static class ProductoEndpoints
{
    public static void MapProductoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Producto").WithTags(nameof(Producto));

        group.MapGet("/", async (McsVecinosContext db) =>
        {
            return await db.Productos.ToListAsync();
        })
        .WithName("GetAllProductos")
        .WithOpenApi()
        .Produces<List<Producto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, McsVecinosContext db) =>
        {
            return await db.Productos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Producto model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductoById")
        .WithOpenApi()
        .Produces<Producto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, Producto producto, McsVecinosContext db) =>
        {
            var affected = await db.Productos
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, producto.Id)
                  .SetProperty(m => m.Nombre, producto.Nombre)
                  .SetProperty(m => m.Descripcion, producto.Descripcion)
                  .SetProperty(m => m.Costo, producto.Costo)
                  .SetProperty(m => m.Precio, producto.Precio)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateProducto")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Producto producto, McsVecinosContext db) =>
        {
            db.Productos.Add(producto);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Producto/{producto.Id}",producto);
        })
        .WithName("CreateProducto")
        .WithOpenApi()
        .Produces<Producto>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, McsVecinosContext db) =>
        {
            var affected = await db.Productos
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteProducto")
        .WithOpenApi()
        .Produces<Producto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
