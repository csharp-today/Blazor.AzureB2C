namespace CSharpToday.Blazor.AzureB2C
{
    internal class B2CConfig : IB2CConfig
    {
        public string LoginEntryPointUrl { get; set; }
        public string OpenIdConfigUrl { get; set; }
    }
}
