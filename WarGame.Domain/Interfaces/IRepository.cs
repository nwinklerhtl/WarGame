using System.Linq.Expressions;
using WarGame.Model.Configuration;

namespace WarGame.Domain.Interfaces;

public interface IRepository<TEntity>
{
    Task<TDetailDto?> GetByIdAsync<TDetailDto>(int id) where TDetailDto : class;
    Task<IEnumerable<TListDto>> GetAsync<TListDto>(int start = 0, int count = 10) where TListDto : class;
    Task<IEnumerable<TListDto>> GetAsync<TListDto>(Expression<Func<TEntity, bool>> filter, int start = 0, int count = 10) where TListDto : class;
    Task<TListDto> AddAsync<TNewDto, TListDto>(TNewDto newDto) 
        where TListDto : class
        where TNewDto : class;
    Task UpdateAsync<TUpdateDto>(int id, TUpdateDto updateDto) where TUpdateDto : class;
    Task<bool> DeleteAsync(int id);
}