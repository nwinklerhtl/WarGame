using Microsoft.AspNetCore.Mvc;
using WarGame.Domain.Interfaces;
using WarGame.Model.Configuration;

namespace WarGame.Api.Endpoints;

public class EndpointBase<TEntity, TListDto, TDetailDto, TNewDto, TUpdateDto> 
    where TListDto : class, IHasIdGetter
    where TDetailDto : class
    where TNewDto : class
    where TUpdateDto : class
{
    private string _groupName = string.Empty;
    
    public RouteGroupBuilder MapEndpoints(IEndpointRouteBuilder routes, string groupName, params string[] tags)
    {
        var group = routes.MapGroup($"/{groupName}").WithTags(tags);
        _groupName = groupName;

        group.MapGet("/", GetList);

        group.MapGet("/{id:int}", GetById);

        group.MapPost("/", Create);

        group.MapPut("/{id:int}", Update);

        group.MapDelete("/{id:int}", Delete);

        return group;
    }
    
    public async Task<IResult> GetList(
        [FromServices] IRepository<TEntity> repo,
        [FromQuery] int start = 0,
        [FromQuery] int count = 10) => 
        Results.Ok(await repo.GetAsync<TListDto>(start, count));

    public async Task<IResult> GetById(
        [FromServices] IRepository<TEntity> repo,
        [FromRoute] int id)
    {
        var entry = await repo.GetByIdAsync<TDetailDto>(id);
        return entry is not null ? Results.Ok(entry) : Results.NotFound();
    }
    
    public async Task<IResult> Create(
        [FromServices] IRepository<TEntity> repo,
        [FromBody] TNewDto dto)
    {
        var created = await repo.AddAsync<TNewDto, TListDto>(dto);
        return Results.Created($"/api/{_groupName}/{created.Id}", created);
    }

    public async Task<IResult> Update(
        [FromRoute] int id,
        [FromServices] IRepository<TEntity> repo,
        [FromBody] TUpdateDto dto)
    {
        await repo.UpdateAsync(id, dto);
        return Results.NoContent();
    }

    public async Task<IResult> Delete(
        [FromServices] IRepository<TEntity> repo,
        [FromRoute] int id) =>
        await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
}