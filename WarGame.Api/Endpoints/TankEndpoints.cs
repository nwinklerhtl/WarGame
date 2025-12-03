using Microsoft.AspNetCore.Mvc;
using WarGame.Domain.Interfaces;
using WarGame.Domain.Mapping;
using WarGame.Model.Models;

namespace WarGame.Api.Endpoints;

public static class TankEndpoints
{
    public static void MapTankEndpoints(this IEndpointRouteBuilder routes)
    {
        var endpoints = new TankEndpointBase();
        // map base endpoints
        var group = endpoints.MapEndpoints(routes, "tanks", "Tank");
        
        // map additional endpoints
        group.MapPost("/fight", endpoints.Fight);
    }

    // when you need additional endpoints:
    private class TankEndpointBase : EndpointBase<Tank, TankListDto, TankDetailDto, TankCreateDto, TankUpdateDto>
    {
        public async Task<IResult> Fight(
            [FromServices] IRepository<Tank> repo,
            [FromQuery] int tankOneId,
            [FromQuery] int tankTwoId) => 
            Results.Ok();
    }
}