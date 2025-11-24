using Facet;
using WarGame.Model.Configuration;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(Tank), 
    Include = [
        nameof(Tank.Id), 
        nameof(Tank.Name)])]
public partial record TankListDto : IHasIdGetter;

[Facet(typeof(Tank), 
    exclude: [
        nameof(Tank.TankWeapons)],
    NestedFacets = [
        typeof(CountryListDto), 
        typeof(FactionListDto),
        typeof(TankTypeListDto),
        typeof(TankStatDetailDto)])]
public partial record TankDetailDto;

[Facet(typeof(Tank), 
    exclude: [
        nameof(Tank.Id),
        nameof(Tank.TankStats), 
        nameof(Tank.TankWeapons),
        nameof(Tank.Country),
        nameof(Tank.Faction),
        nameof(Tank.TankType)])]
public partial record TankCreateDto;

[Facet(typeof(Tank),
    Include = [
        nameof(Tank.Name),
        nameof(Tank.YearIntroduced),
        nameof(Tank.CountryId),
        nameof(Tank.FactionId),
        nameof(Tank.TankTypeId),
        nameof(Tank.TankStats)
    ],
    NestedFacets = [
        typeof(TankStatDetailDto)
    ])]
public partial record TankUpdateDto;