using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class Weapon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public float? CaliberMm { get; set; }

    public float? RateOfFire { get; set; }

    public virtual ICollection<TankWeapon> TankWeapons { get; set; } = new List<TankWeapon>();
}
