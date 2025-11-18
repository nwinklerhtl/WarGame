using Facet;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(TankType), 
    Include = [
        nameof(TankType.Id),
        nameof(TankType.Name),
        nameof(TankType.Description)])]
public partial record TankTypeListDto;