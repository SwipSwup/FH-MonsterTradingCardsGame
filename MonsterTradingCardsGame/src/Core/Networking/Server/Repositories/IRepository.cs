using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonsterTradingCardsGame.Core.Networking.Server.Repositories;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(uint id);
    
    Task<IEnumerable<T>> GetAllAsync();
    
    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(uint id);
}