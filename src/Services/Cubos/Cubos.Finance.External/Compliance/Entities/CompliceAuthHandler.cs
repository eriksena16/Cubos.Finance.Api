using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Cubos.Finance.External
{
    public class CompliceAuthHandler : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _cache;

        public CompliceAuthHandler(IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var options = scope.ServiceProvider.GetRequiredService<IOptions<ComplianceExternalOptions>>().Value;
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<CompliceAuthHandler>>();

            using var tokenClient = new HttpClient { BaseAddress = new Uri(options.BaseAddress) };

            var authRequest = new { email = options.Email, password = options.PassWord };
            var authResponse = await tokenClient.PostAsJsonAsync(options.RequestUriCode, authRequest, cancellationToken);

            if (!authResponse.IsSuccessStatusCode)
            {
                logger.LogError("Failed to get Code from Complice API: {StatusCode}", authResponse.StatusCode);
                throw new Exception("Auth Code request failed");
            }

            var authCode = await authResponse.Content.ReadFromJsonAsync<ApiResponse<AuthCodeResponse>>(cancellationToken: cancellationToken);
            var tokenRequest = new { authCode = authCode.Data.AuthCode };

            var tokenResponse = await tokenClient.PostAsJsonAsync(options.RequestUriToken, tokenRequest, cancellationToken);

            if (!tokenResponse.IsSuccessStatusCode)
            {
                logger.LogError("Failed to get Token from Complice API: {StatusCode}", tokenResponse.StatusCode);
                throw new Exception("Auth Token request failed");
            }

            var token = await tokenResponse.Content.ReadFromJsonAsync<ApiResponse<TokenResponse>>(cancellationToken: cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Data.AccessToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
