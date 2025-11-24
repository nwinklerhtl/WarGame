using Microsoft.AspNetCore.Mvc;
using WarGame.Domain.Interfaces;
using WarGame.Domain.Mapping;
using WarGame.Model.Models;

namespace WarGame.Api.Endpoints;

public static class TankEndpoints
{
    public static void MapTankEndpoints(this IEndpointRouteBuilder routes)
    {
        EndpointBase<Tank, TankListDto, TankDetailDto, TankCreateDto, TankUpdateDto>
            .MapEndpoints(routes, "tanks", "Tank");
    }
}