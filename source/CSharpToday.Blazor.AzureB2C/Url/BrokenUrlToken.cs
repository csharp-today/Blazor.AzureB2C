namespace CSharpToday.Blazor.AzureB2C.Url
{
    public class BrokenUrlToken : UrlToken
    {
        public string Error { get; }
        public string ErrorDescription { get; }

        public BrokenUrlToken(string error, string description) =>
            (Error, ErrorDescription) = (error, description);
    }
}
