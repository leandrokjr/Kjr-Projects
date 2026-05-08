namespace CloudGames.Domain.Inputs;

public class GameInput
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public decimal? PricePromotion { get; set; }
}
