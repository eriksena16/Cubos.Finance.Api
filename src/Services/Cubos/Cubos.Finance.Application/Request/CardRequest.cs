using System.ComponentModel.DataAnnotations;

namespace Cubos.Finance.Application
{
    public class CardRequest
    {
        [Required(ErrorMessage = "O tipo é obrigatório.")]
        [RegularExpression(@"^(virtual|physical)$", ErrorMessage = "O tipo deve ser 'virtual' ou 'physical'.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        [CreditCard(ErrorMessage = "O número do cartão é inválido.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O CVV é obrigatório.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "O CVV deve ter 3 dígitos.")]
        public string Cvv { get; set; }
    }


}
