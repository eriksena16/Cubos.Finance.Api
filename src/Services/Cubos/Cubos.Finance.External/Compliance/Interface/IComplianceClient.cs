using Refit;

namespace Cubos.Finance.External
{

    public interface IComplianceClient
    {
        [Post("/auth/code")]
        Task<AuthCodeResponse> AuthCodeAsync([Body] AuthCodeRequest request);
        [Post("/auth/token")]
        Task<TokenResponse> AuthTokenAsync([Body] TokenRequest request);
        [Post("/auth/refresh")]
        Task<TokenResponse> AuthRefreshTokenAsync([Body] TokenRequest request);
        [Post("/cpf/validate")]
        Task<ApiResponse<ComplianceResponse>> ValidateCpfAsync([Body] DocumentRequest request);
        [Post("/cnpj/validate")]
        Task<ApiResponse<ComplianceResponse>> ValidateCnpjAsync([Body] DocumentRequest request);
    }
}
