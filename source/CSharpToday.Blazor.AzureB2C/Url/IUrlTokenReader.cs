namespace CSharpToday.Blazor.AzureB2C.Url
{
    public interface IUrlTokenReader
    {
        UrlToken GetTokenFromUrl(string url);
    }
}
