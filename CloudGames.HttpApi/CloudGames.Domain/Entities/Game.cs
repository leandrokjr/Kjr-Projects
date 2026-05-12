namespace CloudGames.Domain.Entities;

public class Game : Entity
{
    public required string Title { get; set; }
    public required decimal Price { get; set; }
    public decimal CurrentPrice { get; set; }

    public void ApplyDiscount(int percentage)
    {
        if (percentage < 0 || percentage > 99)
            throw new ArgumentException("O desconto deve estar entre 0 e 99%.");

        var discountValue = Price * percentage / 100;
        CurrentPrice = Price - discountValue;
    }
}
