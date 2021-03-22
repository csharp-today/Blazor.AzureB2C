using CSharpToday.Blazor.AzureB2C.Builders;
using CSharpToday.Blazor.AzureB2C.Url;
using System;
using System.Threading.Tasks;

namespace CSharpToday.Blazor.AzureB2C
{
    internal class TokenInfoFactory : ITokenInfoFactory
    {
        private readonly ITokenBuilder _noValidationBuilder;
        private readonly ITokenValidationBuilder _validationBuilder;

        public TokenInfoFactory(ITokenBuilder noValidationBuilder, ITokenValidationBuilder validationBuilder)
        {
            _noValidationBuilder = noValidationBuilder;
            _validationBuilder = validationBuilder;
        }

        public async Task<ITokenInfo> GetTokenInfoAsync(UrlToken urlToken, bool skipValidation = false)
        {
            const string InvalidTokenMessage = "Autorization token is invalid";
            try
            {
                if (urlToken is ValidUrlToken validUrlToken)
                {
                    return skipValidation
                        ? _noValidationBuilder.Build(validUrlToken.RawToken)
                        : await _validationBuilder.ValidateAndBuildAsync(validUrlToken.RawToken);
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
