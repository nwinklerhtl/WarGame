using Facet.Extensions;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WarGame.Domain.Interfaces;
using WarGame.Model.Configuration;

namespace WarGame.Domain.Implementation;

public class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, IHasId, new()
{
    protected readonly WargameContext DbContext;
    protected readonly DbSet<TEntity> Table;
    
    public RepositoryBase(WargameContext dbContext)
    {
        DbContext = dbContext;
        Table = dbContext.Set<TEntity>();
    }

    public virtual async Task<TDetailDto?> GetByIdAsync<TDetailDto>(int id) where TDetailDto : class
    {
        return await Table
            .Where(r => r.Id == id)
            .SelectFacet<TDetailDto>()
            .FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<TListDto>> GetAsync<TListDto>(int start = 0, int count = 10) where TListDto : class
    {
        count = Math.Min(count, 100); // restrict to 100 entries max
        return await Table
            .Skip(start)
            .Take(count)
            .SelectFacet<TListDto>()
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<TListDto>> GetAsync<TListDto>(Expression<Func<TEntity, bool>> filter, int start = 0, int count = 10) where TListDto : class
    {
        count = Math.Min(count, 100); // restrict to 100 entries max
        return await Table
            .Where(filter)
            .Skip(start)
            .Take(count)
            .SelectFacet<TListDto>()
            .ToListAsync();
    }

    public virtual async Task<TListDto> AddAsync<TNewDto, TListDto>(TNewDto newDto) 
        where TListDto : class
        where TNewDto : class
    {
        var entity = newDto.BackTo<TNewDto, TEntity>();
        await Table.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity.ToFacet<TListDto>();
    }

    public virtual async Task UpdateAsync<TUpdateDto>(int id, TUpdateDto updateDto)
        where TUpdateDto : class
    {
        var toUpdate = updateDto.BackTo<TEntity>();
        toUpdate.Id = id;
        Table.Update(toUpdate);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var nrOfDeletedRows = await Table.Where(e => e.Id == id).ExecuteDeleteAsync();
        return nrOfDeletedRows == 1;
    }
}