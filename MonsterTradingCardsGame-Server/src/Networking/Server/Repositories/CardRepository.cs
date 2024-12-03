using MonsterTradingCardsGame.Gameplay.Card;

namespace MonsterTradingCardsGame.Networking.Server.Repositories;

public class CardRepository : IRepository<uint, Card>
{
    public Task<IEnumerable<Card>> GetAllAsync()
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

    public Task DeleteAsync(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<Card> GetByIdAsync(uint id)
    {
        throw new NotImplementedException();
    }
}