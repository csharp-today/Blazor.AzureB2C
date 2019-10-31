using CSharpToday.Blazor.AzureB2C.Http;
using LucidCode;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CSharpToday.Blazor.AzureB2C.Test.Http
{
    [TestClass]
    public class ExtensionsTest : BaseTest
    {
        [TestMethod]
        public void Build_HttpRequestAuthorizer() => LucidTest
            .Arrange(() => new ServiceCollection())
            .Act(services => services.AddB2CAuthorizationAndVerification(Get<IB2CConfig>()))
            .Assert(services => services
                .BuildServiceProvider()
                .GetService<IHttpRequestAuthorizer>()
                .ShouldNotBeNull());
    }
}
