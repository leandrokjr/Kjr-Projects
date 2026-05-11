using System.ComponentModel.DataAnnotations;

namespace CloudGames.Domain.Inputs;

public class UserPasswordUpdateInput
{
    public Guid Id { get; set; }

    [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "A senha deve conter letras, números e caracteres especiais.")]
    public string CurrentPassword { get; set; }

    [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "A senha deve conter letras, números e caracteres especiais.")]
    public string NewPassword { get; set; }
}
