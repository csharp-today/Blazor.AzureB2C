using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C.Validators
{
    internal interface ITokenValidator
    {
        Task<ITokenInfo> ValidateAsync(string token);
    }
}
