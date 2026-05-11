namespace CloudGames.Domain.Entities;

public class Game : Entity
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public decimal CurrentPrice { get; set; }
}
