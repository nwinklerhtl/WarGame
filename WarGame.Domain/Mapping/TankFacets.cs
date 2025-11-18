using Facet;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(Tank), 
    Include = [
        nameof(Tank.Id), 
        nameof(Tank.Name)])]
public partial record TankListDto;

[Facet(typeof(Tank), 
    exclude: [
        nameof(Tank.TankStats), 
        nameof(Tank.TankWeapons)],
    NestedFacets = [
        typeof(CountryListDto), 
        typeof(FactionListDto),
        typeof(TankTypeListDto)])]
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