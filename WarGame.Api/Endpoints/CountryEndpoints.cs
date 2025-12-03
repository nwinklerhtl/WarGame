using WarGame.Domain.Mapping;
using WarGame.Model.Models;

namespace WarGame.Api.Endpoints;

public static class CountryEndpoints
{
    public static void MapCountryEndpoints(this IEndpointRouteBuilder routes)
    {
        new EndpointBase<Country, CountryListDto, CountryDetailDto, CountryCreateDto, CountryDetailDto>()
            .MapEndpoints(routes, "countries", "Country");
    }
}