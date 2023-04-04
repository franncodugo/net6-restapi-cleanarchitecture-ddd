using DinnerRes.Application.User.Interfaces;

namespace DinnerRes.Infrastructure.User;

public class UserRepository : IUserRepository
{
    private static readonly List<Domain.Entities.User> _users = new();
    
    public void Add(Domain.Entities.User user)
    {
        _users.Add(user);
    }
    
    public Domain.Entities.User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email.Equals(email));
    }
}