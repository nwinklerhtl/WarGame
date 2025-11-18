using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class Faction
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Ideology { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
}
