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

        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            var dbContextSvc = svcProvider.GetRequiredService<FinanceContext>();

            await dbContextSvc.Database.MigrateAsync();
        }
    }
}
