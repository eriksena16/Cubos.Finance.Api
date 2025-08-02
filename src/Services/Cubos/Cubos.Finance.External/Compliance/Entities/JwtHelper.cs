using System.IdentityModel.Tokens.Jwt;

namespace Cubos.Finance.External
{
    public static class JwtHelper
    {
        public static DateTime? GetExpirationDate(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            if (jwtToken.Payload.Expiration.HasValue)
                return null;

            var secondsSinceEpoch = Convert.ToInt64(jwtToken.Payload.Expiration);
            return DateTimeOffset.FromUnixTimeSeconds(secondsSinceEpoch).UtcDateTime;
        }
    }

}
