using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cubos.Finance.Api
{
    public static class JwtConfig
    {
        public static WebApplicationBuilder AddJwtConfig(this WebApplicationBuilder builder)
        {
            var jwtSettingsSection = builder.Configuration.GetSection(nameof(JwtSettings));

            var secretFromEnv = Environment.GetEnvironmentVariable("COMPLICE_SECRET");

            builder.Services.Configure<JwtSettings>(options =>
            {
                jwtSettingsSection.Bind(options);
                if (!string.IsNullOrEmpty(secretFromEnv))
                {
                    options.Secret = secretFromEnv;
                }
            });

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            if (!string.IsNullOrEmpty(secretFromEnv))
            {
                jwtSettings.Secret = secretFromEnv;
            }

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return builder;
        }

    }
}