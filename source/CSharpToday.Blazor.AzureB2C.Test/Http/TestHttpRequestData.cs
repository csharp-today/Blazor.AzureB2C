using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace CSharpToday.Blazor.AzureB2C.Test.Http
{
    internal class TestHttpRequestData : HttpRequestData
    {
        public TestHttpRequestData() : base(new TestFunctionContext()) { }

        public override Stream Body => throw new NotImplementedException();

        public override HttpHeadersCollection Headers { get; } = new HttpHeadersCollection();

        public override IReadOnlyCollection<IHttpCookie> Cookies => throw new NotImplementedException();

        public override Uri Url => throw new NotImplementedException();

        public override IEnumerable<ClaimsIdentity> Identities => throw new NotImplementedException();

        public override string Method => throw new NotImplementedException();

        public override HttpResponseData CreateResponse() => throw new NotImplementedException();
    }
}
