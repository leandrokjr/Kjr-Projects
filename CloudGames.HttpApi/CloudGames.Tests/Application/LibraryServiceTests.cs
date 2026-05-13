using CloudGames.Application.Services;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;
using Moq;

namespace CloudGames.Tests.Application;
public class LibraryServiceTests
{
    private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IGameRepository> _gameRepositoryMock;
    private readonly LibraryService _libraryService;

    public LibraryServiceTests()
    {
        _libraryRepositoryMock = new Mock<ILibraryRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _gameRepositoryMock = new Mock<IGameRepository>();
        _libraryService = new LibraryService(_libraryRepositoryMock.Object, _userRepositoryMock.Object, _gameRepositoryMock.Object);
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

        _libraryRepositoryMock.Setup(r => r.Exists(userId, gameId)).ReturnsAsync(false);

        var result = await _libraryService.AddGameByUser(userId, gameId);

        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);

        _libraryRepositoryMock.Verify(r => r.Create(It.Is<Library>(l =>
            l.UserId == userId && l.GameId == gameId)), Times.Once);

        _userRepositoryMock.Verify(r => r.GetById(userId), Times.Exactly(2));
    }
}

