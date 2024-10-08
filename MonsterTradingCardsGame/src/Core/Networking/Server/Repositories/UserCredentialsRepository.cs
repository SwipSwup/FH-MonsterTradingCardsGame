using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.DTOs.Authentication;
using MonsterTradingCardsGame.Gameplay.User;

namespace MonsterTradingCardsGame.Core.Networking.Server.Repositories;

public class UserCredentialsRepository : IRepository<string, UserCredentialsDto?>
{
    private Dictionary<string, string> _credentials = new();
    
    public async Task<UserCredentialsDto?> GetByIdAsync(string username)
    {
        await Task.CompletedTask;
        if (_credentials.TryGetValue(username, out string password))
        {
            return new UserCredentialsDto { Username = username, Password = password };
        }
        
        return null;
    }

    public Task<IEnumerable<UserCredentialsDto?>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task AddAsync(UserCredentialsDto? entity)
    {
        if(entity is null)
            return;
        
        _credentials[entity.Value.Username] = entity.Value.Password;
        await Task.CompletedTask;
    }

    public Task UpdateAsync(UserCredentialsDto? entity)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new System.NotImplementedException();
    }
}