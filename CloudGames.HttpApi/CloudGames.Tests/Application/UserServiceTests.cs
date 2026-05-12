using CloudGames.Application.Services;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Moq;

namespace CloudGames.Tests.Application;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IGameRepository> _gameRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _gameRepositoryMock = new Mock<IGameRepository>();
        _userService = new UserService(_userRepositoryMock.Object, _gameRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnUserWithHashedPassword()
    {
        var input = new UserCreateInput 
        { 
            Name = "User Create Tester", 
            Email = "usercreate@tester.com", 
            Password = "123A1111B" 
        };

        var result = await _userService.CreateUser(input);

        Assert.NotNull(result);
        Assert.NotEqual("123A1111B", result.Password);
        Assert.True(BCrypt.Net.BCrypt.Verify("123A1111B", result.Password));
        _userRepositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
    {
        _userRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((User)null);

        var result = await _userService.GetUserById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllUser_ShouldReturnNull_WhenRepositoryIsEmpty()
    {
        _userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User>());

        var result = await _userService.GetAllUsers();

        Assert.Null(result);
    }

    [Fact]
    public async Task AddGameByUser_ShouldWork_WhenUserAndGameExist()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();

        var user = new User 
        { 
            Id = userId, 
            Name = "Player", 
            Email = "player@player.com", 
            Password = "7878s4@fgK", 
            Administrator = true 
        };

        var game = new Game 
        { 
            Id = gameId, 
            Title = "Need For Speed Underground II",
            Price = 49
        };

        _userRepositoryMock.Setup(r => r.GetById(userId)).Returns(user);
        _gameRepositoryMock.Setup(r => r.GetById(gameId)).Returns(game);

        var result = await _userService.AddGameByUser(userId, gameId);

        Assert.NotNull(result);
        _userRepositoryMock.Verify(r => r.AddGameByUser(user, game), Times.Once);
        _userRepositoryMock.Verify(r => r.Update(user), Times.Once);
    }

    [Fact]
    public async Task UpdatePassword_ShouldReturnNull_WhenCurrentPasswordIsWrong()
    {
        var id = Guid.NewGuid();
        var hashedOld = BCrypt.Net.BCrypt.HashPassword("V@lidS@nha");
        var user = new User 
        { 
            Id = id, 
            Name = "Tester",
            Email = "tester@tester.com",
            Password = hashedOld,
            Administrator = false
        };

        var input = new UserPasswordUpdateInput
        {
            Id = id,
            CurrentPassword = "1nV@lidS@nha",
            NewPassword = "nov@senh@1"
        };

        _userRepositoryMock.Setup(r => r.GetById(id)).Returns(user);

        var result = await _userService.UpdatePasswordByUser(input);

        Assert.Null(result);
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task UpdateUser_ShouldPassword_WhenUpdatingProfile()
    {
        var id = Guid.NewGuid();
        var originalPassword = "456f4ds54f65ds65fd65s";

        var existingUser = new User 
        { 
            Id = id, 
            Name = "Hasher",
            Email = "hasher@hasher.com",
            Password = originalPassword,
            Administrator = false
        };

        var input = new UserUpdateInput 
        { 
            Id = id, 
            Name = "Hasher Hash",
            Email = "hasher@hasher.com",
        };

        _userRepositoryMock.Setup(r => r.GetById(id)).Returns(existingUser);

        var result = await _userService.UpdateUser(input);

        Assert.Equal(originalPassword, result.Password);
        Assert.Equal("Hasher Hash", result.Name);
        _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_ShouldCallRepositoryDelete_WhenCalled()
    {
        var id = Guid.NewGuid();
        await _userService.DeleteUser(id);

        _userRepositoryMock.Verify(r => r.Delete(id), Times.Once);
    }
}
