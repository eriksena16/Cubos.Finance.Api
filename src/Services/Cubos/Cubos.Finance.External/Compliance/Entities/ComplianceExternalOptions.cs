namespace Cubos.Finance.External
{
    public class ComplianceExternalOptions
    {
        public static string Instance { get; } = "COMPLIANCE";
        public string BaseAddress { get; set; }
        public string RequestUriCode { get; set; }
        public string RequestUriToken { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
    }

}
