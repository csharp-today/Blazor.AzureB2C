using LucidCode;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CSharpToday.Blazor.AzureB2C.Test
{
    [TestClass]
    public class B2CExtensionsTest : BaseTest
    {
        [TestMethod]
        public void Build_All_Services() => LucidTest
            .Arrange(() => new ServiceCollection())
            .Act(services => services.AddB2CAuthorization(Get<IB2CConfig>()))
            .Assert(services =>
            {
                var provider = services.BuildServiceProvider();
                foreach (var service in services)
                {
                    var obj = provider.GetService(service.ServiceType);
                    obj.ShouldNotBeNull();
                }
            });
    }
}
