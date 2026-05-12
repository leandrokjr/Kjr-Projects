using System.ComponentModel.DataAnnotations;

namespace CloudGames.Application.Inputs;

public class GameUpdateInput
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required decimal Price { get; set; }

    [Range(1, 99, ErrorMessage = "O desconto deve ser entre 1% e 99%")]
    public decimal CurrentPrice { get; set; }
}
