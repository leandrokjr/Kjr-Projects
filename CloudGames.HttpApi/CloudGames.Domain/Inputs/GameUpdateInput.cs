namespace CloudGames.Domain.Inputs;

public class GameUpdateInput
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public decimal? PricePromotion { get; set; }
}
