namespace Cubos.Finance.Shared
{
    public static class StringExtensions
    {
        public static bool IsValidCardNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            var digitsOnly = new string(input.Where(char.IsDigit).ToArray());
            return digitsOnly.Length == 16;
        }

        public static string FormatCardNumber(this string input)
        {
            var digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length != 16)
                throw new FormatException("O número do cartão deve ter exatamente 16 dígitos.");

            return string.Join(" ", Enumerable.Range(0, 4)
                .Select(i => digitsOnly.Substring(i * 4, 4)));
        }
    }


}
