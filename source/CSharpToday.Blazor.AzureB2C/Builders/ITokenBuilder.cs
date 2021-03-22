namespace CSharpToday.Blazor.AzureB2C.Builders
{
    internal interface ITokenBuilder
    {
        ITokenInfo Build(string token);
    }
}
