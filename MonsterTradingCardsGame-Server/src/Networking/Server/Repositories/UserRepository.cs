using MonsterTradingCardsGame.Gameplay.User;

namespace MonsterTradingCardsGame.Networking.Server.Repositories;

public class UserRepository : IRepository<uint, User>
{
    Dictionary<uint, User> _users = new();
    
    
    public Task<User> GetByIdAsync(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(uint id)
    {
        throw new NotImplementedException();
    }
}