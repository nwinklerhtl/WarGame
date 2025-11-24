using Facet;
using WarGame.Model.Models;

namespace WarGame.Domain.Mapping;

[Facet(typeof(TankStat),
    exclude: [nameof(Tank)])]
public partial record TankStatDetailDto;