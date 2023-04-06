using DinnerRes.Application.User.Interfaces;
using DinnerRes.Infrastructure.User;
using Moq;

namespace DinnerRest.Tests.Infrastructure.User;

public class UserRepositoryTests
{
    private readonly UserRepository _userRepository = new();
    
    [Fact]
    public void Add_SavesUserSuccessfully()
    {
        // Arrange
        var userMock = new DinnerRes.Domain.Entities.User()
        {
            FirstName = "Franco",
            LastName = "Dugo",
            Email = "fdugo@mail.com",
            Password = "213dasxasd"
        };

        // Act
        _userRepository.Add(userMock);
        var userList = _userRepository.GetAll();
        
        // Assert
        var collection = userList.ToList();
        Assert.NotEmpty(collection);
        Assert.IsType<List<DinnerRes.Domain.Entities.User>>(userList);
        var singleUser = Assert.Single(collection);
        Assert.NotNull(singleUser);
        Assert.Equal("Franco", singleUser.FirstName);
        Assert.Equal("Dugo", singleUser.LastName);
        Assert.Equal("fdugo@mail.com", singleUser.Email);
        Assert.Equal("213dasxasd", singleUser.Password);
    }
    
    [Fact]
    public void GetUserByEmail_ReturnsTheUserWhenExists()
    {
        // Arrange
        var userMock = new DinnerRes.Domain.Entities.User()
        {
            FirstName = "Franco",
            LastName = "Dugo",
            Email = "fdugo@mail.com",
            Password = "213dasxasd"
        };
        _userRepository.Add(userMock);

        // Act
        var user = _userRepository.GetUserByEmail(userMock.Email);
        
        // Assert
        Assert.NotNull(user);
        Assert.IsType<DinnerRes.Domain.Entities.User>(user);
        Assert.Equal("Franco", user.FirstName);
        Assert.Equal("Dugo", user.LastName);
        Assert.Equal("fdugo@mail.com", user.Email);
        Assert.Equal("213dasxasd", user.Password);
    }
    
    [Fact]
    public void GetUserByEmail_ReturnsNullWhenUserDoesntExists()
    {
        // Arrange
        var mockFailEmailAcc = "fake@mock.com";

        // Act
        var user = _userRepository.GetUserByEmail(mockFailEmailAcc);
        
        // Assert
        Assert.Null(user);
    }
}

