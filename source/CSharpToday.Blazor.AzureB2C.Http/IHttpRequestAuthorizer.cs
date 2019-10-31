using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C.Http
{
    public interface IHttpRequestAuthorizer
    {
        Task<ITokenInfo> GetAuthTokenAsync(HttpRequest request, ILogger log);
    }
}
