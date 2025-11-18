using Facet;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(Faction), 
    Include = [
        nameof(Faction.Id), 
        nameof(Faction.Name), 
        nameof(Faction.Ideology)])]
public partial record FactionListDto;