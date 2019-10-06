using Ninject;
using Ninject.MockingKernel.NSubstitute;
using System;

namespace CSharpToday.Blazor.AzureB2C.Test
{
    public class BaseTest : IDisposable
    {
        private readonly NSubstituteMockingKernel _kernel = new NSubstituteMockingKernel();

        public void Dispose() => _kernel.Dispose();

        protected T Get<T>() => _kernel.Get<T>();
    }
}
