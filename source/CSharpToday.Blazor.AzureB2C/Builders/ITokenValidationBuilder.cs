using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C.Builders
{
    internal interface ITokenValidationBuilder
    {
        Task<ITokenInfo> ValidateAndBuildAsync(string token);
    }
}
