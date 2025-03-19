using Rush.Application.Interfaces.Base;
using Rush.Domain.Entities;

namespace Rush.Application.Interfaces.User;

public interface IUsersService
{
    public Task<string> GetRoleName(Guid Id);
}