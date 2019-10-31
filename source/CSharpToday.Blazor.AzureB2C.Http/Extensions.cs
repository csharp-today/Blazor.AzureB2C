using Microsoft.Extensions.DependencyInjection;

namespace CSharpToday.Blazor.AzureB2C.Http
{
    public static class Extensions
    {
        public static IServiceCollection AddB2CAuthorizationAndVerification(this IServiceCollection services, IB2CConfig config) => services
            .AddB2CAuthorization(config)
            .AddSingleton<IHttpRequestAuthorizer, HttpRequestAuthorizer>();
    }
}
