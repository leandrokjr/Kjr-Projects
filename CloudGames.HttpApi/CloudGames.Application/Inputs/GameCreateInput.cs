namespace CloudGames.Application.Inputs;

public class GameCreateInput
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public decimal? CurrentPrice { get; set; }
}
