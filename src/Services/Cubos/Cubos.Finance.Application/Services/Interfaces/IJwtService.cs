namespace Cubos.Finance.Application
{
    public interface IJwtService
    {
        BearerToken GenerateAccessToken(string peopleId);
        string GenerateRefreshToken();
    }

}
