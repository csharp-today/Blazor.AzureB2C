using System;

namespace CSharpToday.Blazor.AzureB2C
{
    public interface ITokenInfo
    {
        DateTime Expiration { get; }
        string GivenName { get; }
        string RawToken { get; }
        Guid UserId { get; }
        string UserName { get; }
    }
}
