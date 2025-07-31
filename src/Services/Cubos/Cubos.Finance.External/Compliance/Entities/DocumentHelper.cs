namespace Cubos.Finance.External
{
    public static class DocumentHelper
    {
        public static string CleanDocument(string document)
        {
            return new string(document?.Where(char.IsDigit).ToArray() ?? []);
        }

        public static bool IsCpf(string document)
        {
            var digits = CleanDocument(document);
            return digits.Length == 11;
        }

        public static bool IsCnpj(string document)
        {
            var digits = CleanDocument(document);
            return digits.Length == 14;
        }
    }

}
