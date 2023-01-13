using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace CSharpToday.Blazor.AzureB2C.Http
{
    public interface IHttpRequestAuthorizer
    {
        Task<ITokenInfo> GetAuthTokenAsync(HttpRequestData request, ILogger log);
    }
}
