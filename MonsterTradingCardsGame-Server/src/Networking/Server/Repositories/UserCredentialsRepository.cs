using MonsterTradingCardsGame.Networking.DTOs.Authentication;

namespace MonsterTradingCardsGame.Networking.Server.Repositories;

public class UserCredentialsRepository : IRepository<string, UserCredentialsDto?>
{
    private readonly Dictionary<string, string?> _credentials = new();
    
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}