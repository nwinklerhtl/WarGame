using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class TankWeapon
{
    public int Id { get; set; }

    public int? TankId { get; set; }

    public int? WeaponId { get; set; }

    public string? MountPosition { get; set; }

    public int? AmmoCount { get; set; }

    public virtual Tank? Tank { get; set; }

    public virtual Weapon? Weapon { get; set; }
}
