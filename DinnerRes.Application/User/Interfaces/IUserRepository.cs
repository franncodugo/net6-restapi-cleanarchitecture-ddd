namespace DinnerRes.Application.User.Interfaces;

public interface IUserRepository
{
    Domain.Entities.User? GetUserByEmail(string email);
    void Add(Domain.Entities.User user);
    IEnumerable<Domain.Entities.User> GetAll();
}