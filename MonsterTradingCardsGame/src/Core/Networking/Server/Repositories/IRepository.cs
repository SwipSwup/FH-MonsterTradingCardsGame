using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Gameplay.User;

namespace MonsterTradingCardsGame.Core.Networking.Server.Repositories;

public interface IRepository<in T, T2>
{
    public Task<T2> GetByIdAsync(T id);
    
    public Task<IEnumerable<T2>> GetAllAsync();
    
    public Task AddAsync(T2 entity);
    
    public Task UpdateAsync(T2 entity);
    
    public Task DeleteAsync(T id);
}