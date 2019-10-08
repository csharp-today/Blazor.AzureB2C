using System;
using System.IdentityModel.Tokens.Jwt;

namespace CSharpToday.Blazor.AzureB2C
{
    internal class TokenInfo : ITokenInfo
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private JwtPayload _payload;

        public DateTime Expiration => Epoch.AddSeconds(Convert.ToInt64(_payload["exp"])).ToLocalTime();
        public string GivenName => _payload["given_name"].ToString();
        public string RawToken { get; }
        public Guid UserId => Guid.Parse(_payload["oid"].ToString());
        public string UserName => _payload["name"].ToString();

        internal TokenInfo(string rawToken, JwtPayload jwtPayload)
        {
            if (string.IsNullOrWhiteSpace(rawToken))
            {
                throw new ArgumentNullException(nameof(rawToken), $"Parameter {nameof(rawToken)} can't be empty.");
            }
            if (jwtPayload is null)
            {
                throw new ArgumentNullException(nameof(jwtPayload), $"Parameter {nameof(jwtPayload)} can't be empty.");
            }

            RawToken = rawToken;
            _payload = jwtPayload;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var second = obj as ITokenInfo;
            if (second is null)
            {
                return false;
            }

            return RawToken == second.RawToken;
        }

        public override int GetHashCode() => RawToken.GetHashCode();
    }
}
