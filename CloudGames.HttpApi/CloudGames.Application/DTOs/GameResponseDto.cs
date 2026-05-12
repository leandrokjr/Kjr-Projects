namespace CloudGames.Application.DTOs;

public class GameResponseDto
{
    public GameResponseDto(Guid id, string title, decimal price, decimal currentPrice)
    {
        Id = id;
        Title = title;
        Price = price;
        CurrentPrice = currentPrice;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public decimal CurrentPrice { get; set; }
}
