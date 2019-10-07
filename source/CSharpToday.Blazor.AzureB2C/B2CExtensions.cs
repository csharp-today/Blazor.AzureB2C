using CSharpToday.Blazor.AzureB2C.Url;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class B2CExtensions
    {
        public static IServiceCollection AddB2CAuthorization(this IServiceCollection services) => services
            .AddSingleton<IUrlTokenReader, UrlTokenReader>();
    }
}
