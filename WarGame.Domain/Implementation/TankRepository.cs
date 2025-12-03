using Microsoft.EntityFrameworkCore;
using WarGame.Model.Configuration;
using WarGame.Model.Models;

namespace WarGame.Domain.Implementation;

public class TankRepository(WargameContext dbContext) : RepositoryBase<Tank>(dbContext)
{
    // the tank repository should also delete linked entities (TankStats, TankWeapons)
    public override async Task<bool> DeleteAsync(int id)
    {
        var toDelete = await Table
            .Include(t => t.TankStats)
            .Include(t => t.TankWeapons)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (toDelete is null) return false;
            
        DbContext.TankStats.RemoveRange(toDelete.TankStats);
        DbContext.TankWeapons.RemoveRange(toDelete.TankWeapons);
        DbContext.Tanks.Remove(toDelete);
        await DbContext.SaveChangesAsync();
        return true;
    }
}