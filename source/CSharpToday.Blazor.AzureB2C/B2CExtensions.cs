using CSharpToday.Blazor.AzureB2C;
using CSharpToday.Blazor.AzureB2C.Builders;
using CSharpToday.Blazor.AzureB2C.Url;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class B2CExtensions
    {
        public static IServiceCollection AddB2CAuthorization(this IServiceCollection services, IB2CConfig config) => services
            .AddSingleton(config)
            .AddSingleton<JwtSecurityTokenHandler>()
            .AddSingleton<ITokenBuilder, TokenBuilder>()
            .AddSingleton<ITokenInfoFactory, TokenInfoFactory>()
            .AddSingleton<ITokenValidationBuilder, TokenValidationBuilder>()
            .AddSingleton<IUrlTokenReader, UrlTokenReader>();

        public static IServiceCollection AddB2CAuthorization(this IServiceCollection services, string loginEntryPointUrl, string openIdConfigUrl) =>
            AddB2CAuthorization(services, new B2CConfig { LoginEntryPointUrl = loginEntryPointUrl, OpenIdConfigUrl = openIdConfigUrl });
    }
}
