using Cubos.Finance.Data;
using Microsoft.EntityFrameworkCore;

namespace Cubos.Finance.Api
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            builder.Services
                .AddDbContextFactory<FinanceContext>(options =>
                options.UseNpgsql(connectionString)
                );

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return builder;
        }

    }
}
