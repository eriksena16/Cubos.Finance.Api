namespace Cubos.Finance.Application
{
    public interface IAuthService
    {
        Task<BearerToken> AuthenticateAsync(LoginRequest request);
    }
}
