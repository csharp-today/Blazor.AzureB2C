namespace CSharpToday.Blazor.AzureB2C
{
    public interface IB2CConfig
    {
        string LoginEntryPointUrl { get; }
        string OpenIdConfigUrl { get; }
    }
}
