using CloudGames.Application.Services;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;
using Moq;

public class GameServiceTests
{
    private readonly Mock<IGameRepository> _repositoryMock;
    private readonly GameService _gameService;

    public GameServiceTests()
    {
        _repositoryMock = new Mock<IGameRepository>();
        _gameService = new GameService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateGame_ShouldReturnValidGame_WhenInputIsCorrect()
    {
        var input = new GameCreateInput { Title = "The Last of Us", Price = 299 };

        var result = await _gameService.CreateGame(input);

        Assert.NotNull(result);
        Assert.Equal(input.Title, result.Title);
        Assert.Equal(input.Price, result.CurrentPrice);
        _repositoryMock.Verify(r => r.Create(It.IsAny<Game>()), Times.Once);
    }

    [Fact]
    public async Task GetGameById_ShouldReturnNull_WhenGameDoesNotExist()
    {
        _repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Game)null);

        var result = await _gameService.GetGameById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllGames_ShouldReturnNull_WhenRepositoryIsEmpty()
    {
        _repositoryMock.Setup(r => r.GetAll()).Returns(new List<Game>());

        var result = await _gameService.GetAllGames();

        Assert.Null(result);
    }

    [Theory]
    [InlineData(180, "GTA VI", 10, 162)]
    [InlineData(150, "FIFA 26", 50, 75)]
    public async Task CreateGamePromotion_ShouldUpdatePrice_WhenGameExists(decimal price, string title, int percentage, decimal expectedPrice)
    {
        var id = Guid.NewGuid();
        var game = new Game { Id = id, Title = title, Price = price, CurrentPrice = price };

        _repositoryMock.Setup(r => r.GetById(id)).Returns(game);

        var result = await _gameService.CreateGamePromotion(id, percentage);

        Assert.NotNull(result);
        Assert.Equal(expectedPrice, result.CurrentPrice);
        _repositoryMock.Verify(r => r.Update(It.Is<Game>(g => g.Id == id)), Times.Once);
    }

    [Fact]
    public async Task UpdateGame_ShouldReturnNull_WhenGameDoesNotExist()
    {
        var input = new GameUpdateInput 
        {   
            Id = Guid.NewGuid(), 
            Title = "Inexistente",
            Price = 250,
            CurrentPrice = 250
        };
        _repositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Game)null);

        var result = await _gameService.UpdateGame(input);

        Assert.Null(result);
        _repositoryMock.Verify(r => r.Update(It.IsAny<Game>()), Times.Never);
    }

    [Fact]
    public async Task DeleteGame_ShouldCallRepositoryDelete_WhenCalled()
    {
        var id = Guid.NewGuid();
        await _gameService.DeleteGame(id);

        _repositoryMock.Verify(r => r.Delete(id), Times.Once);
    }
}