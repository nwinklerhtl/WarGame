using System;
using System.Collections.Generic;

namespace WarGame.Model.Models;

public partial class Tank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? YearIntroduced { get; set; }

    public int? CountryId { get; set; }

    public int? FactionId { get; set; }

    public int? TankTypeId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Faction? Faction { get; set; }

    public virtual ICollection<TankStat> TankStats { get; set; } = new List<TankStat>();

    public virtual TankType? TankType { get; set; }

    public virtual ICollection<TankWeapon> TankWeapons { get; set; } = new List<TankWeapon>();
}
