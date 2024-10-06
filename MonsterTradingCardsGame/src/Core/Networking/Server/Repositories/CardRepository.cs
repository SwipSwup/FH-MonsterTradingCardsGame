using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Gameplay.Card;

namespace MonsterTradingCardsGame.Core.Networking.Server.Repositories;

public class CardRepository : IRepository<Card>
{
    Task<IEnumerable<Card>> IRepository<Card>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Card entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Card entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<Card> IRepository<Card>.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}