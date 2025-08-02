namespace Cubos.Finance.Application
{
    public interface IRefreshTokenService
    {
        Task<string> RefreshTokenAsync(string refreshToken);
    }
}
