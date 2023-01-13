using CSharpToday.Blazor.AzureB2C.Url;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C.Http
{
    internal class HttpRequestAuthorizer : IHttpRequestAuthorizer
    {
        public const string AuthorizationHeader = "Authorization";
        private const string AuthorizationHeaderBeginning = "Bearer ";

        private readonly ITokenInfoFactory _tokenFactory;

        public HttpRequestAuthorizer(ITokenInfoFactory tokenFactory) => _tokenFactory = tokenFactory;

        public async Task<ITokenInfo> GetAuthTokenAsync(HttpRequestData request, ILogger log)
        {
            if (request is null || !request.Headers.Contains(AuthorizationHeader))
            {
                log?.LogWarning($"Missing {AuthorizationHeader}");
                return null;
            }

            var header = request.Headers.GetValues(AuthorizationHeader).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(header))
            {
                log?.LogWarning($"Missing {AuthorizationHeader}");
                return null;
            }

            return await GetAuthTokenAsync(header, log);
        }

        private async Task<ITokenInfo> GetAuthTokenAsync(string authHeader, ILogger log)
        {
            if (!authHeader.StartsWith(AuthorizationHeaderBeginning))
            {
                log?.LogWarning($"Invalid {AuthorizationHeader}");
                return null;
            }

            var token = authHeader.Substring(AuthorizationHeaderBeginning.Length);
            if (string.IsNullOrWhiteSpace(token))
            {
                log?.LogWarning($"Invalid {AuthorizationHeader}");
                return null;
            }

            try
            {
                var tokenInfo = await _tokenFactory.GetTokenInfoAsync(new ValidUrlToken(token));
                if (tokenInfo.Expiration < DateTime.Now)
                {
                    log?.LogWarning($"Authorization token expired");
                    return null;
                }

                return tokenInfo;
            }
            catch (Exception ex)
            {
                log?.LogWarning("Authorization error: " + ex);
                return null;
            }
        }
    }
}
