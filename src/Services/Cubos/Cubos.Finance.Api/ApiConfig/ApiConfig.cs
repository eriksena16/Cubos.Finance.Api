using Cubos.Finance.External;
using Microsoft.Extensions.Options;
using Refit;

namespace Cubos.Finance.Api
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                            .ConfigureApiBehaviorOptions(options =>
                            {
                                options.SuppressModelStateInvalidFilter = true;
                            });

            builder.Services.Configure<ComplianceExternalOptions>(
                 builder.Configuration.GetSection(nameof(ComplianceExternalOptions)));

            builder.Services.Configure<ComplianceExternalOptions>(options =>
            {
                builder.Configuration.GetSection(nameof(ComplianceExternalOptions)).Bind(options);

                options.PassWord = Environment.GetEnvironmentVariable("COMPLICE_PASSWORD");
            });

            builder.Services.AddRefitClient<IComplianceClient>()
                    .ConfigureHttpClient((serviceProvider, client) =>
                    {
                        var options = serviceProvider
                            .GetRequiredService<IOptions<ComplianceExternalOptions>>().Value;

                        client.BaseAddress = new Uri(options.BaseAddress);
                    }).AddHttpMessageHandler<CompliceAuthHandler>();

            builder.Services.RegisterServices();

            return builder;
        }
    }
}
