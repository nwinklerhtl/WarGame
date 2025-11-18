using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class TankType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Tank> Tanks { get; set; } = new List<Tank>();
}
