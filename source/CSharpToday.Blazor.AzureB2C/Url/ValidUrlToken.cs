namespace CSharpToday.Blazor.AzureB2C.Url
{
    public class ValidUrlToken : UrlToken
    {
        public string RawToken { get; }

        public ValidUrlToken(string rawToken) => RawToken = rawToken;
    }
}
