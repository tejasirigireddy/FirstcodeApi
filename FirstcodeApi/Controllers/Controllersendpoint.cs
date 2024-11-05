using Microsoft.EntityFrameworkCore;
using FirstcodeApi;
using FirstcodeApi.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Authorization;

namespace FirstcodeApi.Controllers;

public static class Controllersendpoint
{
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder routes)
    {
        // Apply authorization to all endpoints in this group
        var group = routes.MapGroup("/api/Employee")
                          .WithTags(nameof(Employee))
                          .RequireAuthorization(); // Secures all endpoints in this group

        group.MapGet("/", async (DataContext db) =>
        {
            return await db.Employees.ToListAsync();
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Employee>, NotFound>> (int id, DataContext db) =>
        {
            return await db.Employees.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Employee model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEmployeeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Employee employee, DataContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, employee.Id)
                    .SetProperty(m => m.FirstName, employee.FirstName)
                    .SetProperty(m => m.LastName, employee.LastName)
                );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmployee")
        .WithOpenApi();

        group.MapPost("/", async (Employee employee, DataContext db) =>
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Employee/{employee.Id}", employee);
        })
        .WithName("CreateEmployee")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, DataContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmployee")
        .WithOpenApi();
    }
}
