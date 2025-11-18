using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class TankStat
{
    public int Id { get; set; }

    public int? TankId { get; set; }

    public float? ArmorThickness { get; set; }

    public float? TopSpeed { get; set; }

    public float? Weight { get; set; }

    public float? EnginePower { get; set; }

    public virtual Tank? Tank { get; set; }
}
