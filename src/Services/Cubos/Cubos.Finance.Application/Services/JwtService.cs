using Cubos.Finance.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cubos.Finance.Application
{
    public class JwtService(IOptions<JwtSettings> jwtSettings, IConfiguration config) : IJwtService
    {
        private readonly IConfiguration _config = config;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        public BearerToken GenerateAccessToken(string peopleId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, peopleId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(60);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );


            var bearerToken = new BearerToken
            {
                Token = $"Bearer {new JwtSecurityTokenHandler().WriteToken(token)}" ,
            };
            return bearerToken;
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }

}
