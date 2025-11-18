using Microsoft.AspNetCore.Mvc;
using WarGame.Domain.Interfaces;
using WarGame.Domain.Mapping;
using WarGame.Model.Models;

namespace WarGame.Api.Endpoints;

public static class TankEndpoints
{
    public static void MapTankEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/tanks").WithTags("Tanks");

        group.MapGet("/", GetList);

        group.MapGet("/{id:int}", GetById);

        group.MapPost("/", Create);

        group.MapDelete("/{id:int}", Delete);
    }
    
    public static async Task<IResult> GetList(
        [FromServices] IRepository<Tank> repo,
        [FromQuery] int start = 0,
        [FromQuery] int count = 10) => 
        Results.Ok(await repo.GetAsync<TankListDto>(start, count));

    public static async Task<IResult> GetById(
        [FromServices] IRepository<Tank> repo,
        [FromRoute] int id)
    {
        var product = await repo.GetByIdAsync<TankDetailDto>(id);
        return product is not null ? Results.Ok(product) : Results.NotFound();
    }
    
    public static async Task<IResult> Create(
        [FromServices] IRepository<Tank> repo,
        [FromBody] TankCreateDto dto)
    {
        var created = await repo.AddAsync<TankCreateDto, TankListDto>(dto);
        return Results.Created($"/api/tanks/{created.Id}", created);
    }

    public static async Task<IResult> Delete(
        [FromServices] IRepository<Tank> repo,
        [FromRoute] int id) =>
        await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
}