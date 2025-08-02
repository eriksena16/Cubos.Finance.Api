using System.ComponentModel.DataAnnotations;

namespace Cubos.Finance.Application
{
    public class BankAccountRequest
    {
        [Required(ErrorMessage = "Branch é obrigatório.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Branch deve conter exatamente 3 dígitos.")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Account é obrigatório.")]
        [RegularExpression(@"^\d{7}-\d{1}$", ErrorMessage = "Account deve seguir o formato XXXXXXX-X.")]
        public string Account { get; set; }
    }
}
