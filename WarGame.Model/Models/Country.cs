using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Continent { get; set; }

    public virtual ICollection<Faction> Factions { get; set; } = new List<Faction>();

    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
}
