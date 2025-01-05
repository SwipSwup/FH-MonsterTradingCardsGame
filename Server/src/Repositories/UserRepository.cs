using Shared.Networking.DTOs.User;

namespace Server.Repositories;

public class UserRepository : IRepository<Guid, UserDto>
{
    public Task<UserDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserDto entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}