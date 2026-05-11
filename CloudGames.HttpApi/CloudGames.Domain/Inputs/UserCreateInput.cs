using System.ComponentModel.DataAnnotations;

namespace CloudGames.Domain.Inputs;

public class UserCreateInput
{
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
    public required string Email { get; set; }


    [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "A senha deve conter letras, números e caracteres especiais.")]
    public required string Password { get; set; }

    public bool Administrator { get; set; }
}
