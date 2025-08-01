using Cubos.Finance.Shared;

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
            if (DocumentHelper.IsCpf(document))
            {
                var response = await _client.ValidateCpfAsync(new DocumentRequest { Document = document });
                return response.Data.Status == 1;
            }
            else if (DocumentHelper.IsCnpj(document))
            {
                var response = await _client.ValidateCnpjAsync(new DocumentRequest { Document = document });
                return response.Data.Status == 1;
            }

            throw new ArgumentException("Documento inválido: deve conter 11 (CPF) ou 14 (CNPJ) dígitos.");
        }
    }
}
