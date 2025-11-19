using Microsoft.AspNetCore.Mvc;
using WarGame.Domain.Interfaces;
using WarGame.Model.Configuration;

namespace WarGame.Api.Endpoints;

public static class EndpointBase<TEntity, TListDto, TDetailDto, TNewDto> 
    where TListDto : class, IHasId
    where TDetailDto : class
{
    private static string _groupName = string.Empty;
    
    public static void MapEndpoints(IEndpointRouteBuilder routes, string groupName, params string[] tags)
    {
        var group = routes.MapGroup($"/{groupName}").WithTags(tags);
        _groupName = groupName;

        group.MapGet("/", GetList);

        group.MapGet("/{id:int}", GetById);

        group.MapPost("/", Create);

        group.MapDelete("/{id:int}", Delete);
    }
    
    public static async Task<IResult> GetList(
        [FromServices] IRepository<TEntity> repo,
        [FromQuery] int start = 0,
        [FromQuery] int count = 10) => 
        Results.Ok(await repo.GetAsync<TListDto>(start, count));

    public static async Task<IResult> GetById(
        [FromServices] IRepository<TEntity> repo,
        [FromRoute] int id)
    {
        var entry = await repo.GetByIdAsync<TDetailDto>(id);
        return entry is not null ? Results.Ok(entry) : Results.NotFound();
    }
    
    public static async Task<IResult> Create(
        [FromServices] IRepository<TEntity> repo,
        [FromBody] TNewDto dto)
    {
        var created = await repo.AddAsync<TNewDto, TListDto>(dto);
        return Results.Created($"/api/{_groupName}/{created.Id}", created);
    }

    public static async Task<IResult> Delete(
        [FromServices] IRepository<TEntity> repo,
        [FromRoute] int id) =>
        await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
}