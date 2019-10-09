using CSharpToday.Blazor.AzureB2C.Url;
using CSharpToday.Blazor.AzureB2C.Validators;
using System;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C
{
    internal class TokenInfoFactory : ITokenInfoFactory
    {
        private readonly ITokenValidator _validator;

        public TokenInfoFactory(ITokenValidator validator) => _validator = validator;

        public async Task<ITokenInfo> GetTokenInfoAsync(UrlToken urlToken)
        {
            const string InvalidTokenMessage = "Autorization token is invalid";
            try
            {
                if (urlToken is ValidUrlToken validUrlToken)
                {
                    return await _validator.ValidateAsync(validUrlToken.RawToken);
                }

                throw new ApplicationException(InvalidTokenMessage);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(InvalidTokenMessage, ex);
            }
        }
    }
}
