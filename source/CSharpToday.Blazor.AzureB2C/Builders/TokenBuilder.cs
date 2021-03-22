using System.IdentityModel.Tokens.Jwt;

namespace CSharpToday.Blazor.AzureB2C.Builders
{
    internal class TokenBuilder : ITokenBuilder
    {
        private readonly JwtSecurityTokenHandler _jwtHandler;

        public TokenBuilder(JwtSecurityTokenHandler jwtHandler) => _jwtHandler = jwtHandler;

        public ITokenInfo Build(string token)
        {
            var jwtToken = _jwtHandler.ReadJwtToken(token);
            return new TokenInfo(token, jwtToken.Payload);
        }
    }
}
