namespace Cubos.Finance.External
{

    public class ComplianceFacade : IComplianceFacade
    {
        private readonly IComplianceClient _client;

        public ComplianceFacade(IComplianceClient client)
        {
            _client = client;
        }

        public async Task<bool> IsDocumentValidAsync(string document)
        {
            var cleanDoc = DocumentHelper.CleanDocument(document);

            if (DocumentHelper.IsCpf(cleanDoc))
            {
                var response = await _client.ValidateCpfAsync(new DocumentRequest { Document = cleanDoc });
                return response.Status == 1;
            }
            else if (DocumentHelper.IsCnpj(cleanDoc))
            {
                var response = await _client.ValidateCnpjAsync(new DocumentRequest { Document = cleanDoc });
                return response.Status == 1;
            }

            throw new ArgumentException("Documento inválido: deve conter 11 (CPF) ou 14 (CNPJ) dígitos.");
        }
    }
}
