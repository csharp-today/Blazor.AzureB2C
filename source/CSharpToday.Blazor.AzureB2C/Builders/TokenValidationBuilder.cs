using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C.Builders
{
    internal class TokenValidationBuilder : ITokenValidationBuilder
    {
        private readonly IB2CConfig _config;
        private readonly JwtSecurityTokenHandler _jwtHandler;

        private OpenIdConnectConfiguration _configCache;

        public TokenValidationBuilder(IB2CConfig config, JwtSecurityTokenHandler jwtHandler) => (_config, _jwtHandler) = (config, jwtHandler);

        public async Task<ITokenInfo> ValidateAndBuildAsync(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKeys = (await GetConfigAsync()).SigningKeys,
                ValidateLifetime = false
            };

            _jwtHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var payload = (validatedToken as JwtSecurityToken).Payload;
            return new TokenInfo(token, payload);
        }

        private async Task<OpenIdConnectConfiguration> GetConfigAsync()
        {
            if (!(_configCache is null))
            {
                return _configCache;
            }

            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                _config.OpenIdConfigUrl, new OpenIdConnectConfigurationRetriever());
            _configCache = await configManager.GetConfigurationAsync();
            return _configCache;
        }
    }
}
