using System.ComponentModel.DataAnnotations;

namespace Cubos.Finance.Application
{
    public class LoginRequest
    {
        public string Document { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }

}
