using CSharpToday.Blazor.AzureB2C.Url;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C
{
    public interface ITokenInfoFactory
    {
        Task<ITokenInfo> GetTokenInfoAsync(UrlToken urlToken, bool skipValidation = false);
    }
}
