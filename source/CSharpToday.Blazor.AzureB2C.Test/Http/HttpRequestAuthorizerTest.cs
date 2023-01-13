using CSharpToday.Blazor.AzureB2C.Http;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Shouldly;
using NSubstitute;
using CSharpToday.Blazor.AzureB2C.Url;
using System;
using NSubstitute.Core;
using Microsoft.Azure.Functions.Worker.Http;

namespace CSharpToday.Blazor.AzureB2C.Test.Http
{
    [TestClass]
    public class HttpRequestAuthorizerTest : BaseTest
    {
        [TestMethod]
        public async Task Pass_Token_To_Factory() => await LucidTest
            .DefineExpected(() =>
            {
                var token = Get<ITokenInfo>();
                token.Expiration.Returns(DateTime.Now.AddDays(1));
                return token;
            })
            .Arrange(token =>
            {
                const string TextToken = "SOME-TOKEN";
                Get<ITokenInfoFactory>()
                    .GetTokenInfoAsync(null)
                    .ReturnsForAnyArgs(ci => ci.Arg<ValidUrlToken>().RawToken == TextToken ? token : null);
                return GenerateAuthorizationHeader("Bearer " + TextToken);
            })
            .ActAsync(GetTokenAsync)
            .AssertAsync((expectedToken, tokenInfo) => tokenInfo.ShouldBe(expectedToken));

        [TestMethod]
        public Task Return_Null_When_Authorization_Header_Is_Not_Bearer() => LucidTest
            .Arrange(() => GenerateAuthorizationHeader("Some unknown header type"))
            .ActAsync(GetTokenAsync)
            .AssertAsync(TokenShouldBeNull);

        [TestMethod]
        public Task Return_Null_When_Empty_Request() => LucidTest
            .Arrange(() => new TestHttpRequestData())
            .ActAsync(GetTokenAsync)
            .AssertAsync(TokenShouldBeNull);

        [TestMethod]
        public Task Return_Null_When_Factory_Fails() => LucidTest
            .Arrange(() =>
            {
                Get<ITokenInfoFactory>()
                    .GetTokenInfoAsync(null)
                    .ReturnsForAnyArgs(new Func<CallInfo, ITokenInfo>(_ => throw new Exception()));
                return GenerateAuthorizationHeader();
            })
            .ActAsync(GetTokenAsync)
            .AssertAsync(TokenShouldBeNull);

        [TestMethod]
        public Task Return_Null_When_Request_Is_Null() => LucidTest
            .ActAsync(() => Get<HttpRequestAuthorizer>().GetAuthTokenAsync(null, null))
            .AssertAsync(TokenShouldBeNull);

        [TestMethod]
        public Task Return_Null_When_Token_Expires() => LucidTest
            .Arrange(() =>
            {
                var token = Get<ITokenInfo>();
                token.Expiration.Returns(DateTime.Now.AddDays(-1));
                Get<ITokenInfoFactory>().GetTokenInfoAsync(null).ReturnsForAnyArgs(token);
                return GenerateAuthorizationHeader();
            })
            .ActAsync(GetTokenAsync)
            .AssertAsync(TokenShouldBeNull);

        private HttpRequestData GenerateAuthorizationHeader(string header = "Bearer TEST-TOKEN")
        {
            var request = new TestHttpRequestData();
            request.Headers.Add(HttpRequestAuthorizer.AuthorizationHeader, header);
            return request;
        }

        private Task<ITokenInfo> GetTokenAsync(HttpRequestData request) => Get<HttpRequestAuthorizer>().GetAuthTokenAsync(request, null);

        private void TokenShouldBeNull(ITokenInfo token) => token.ShouldBeNull();
    }
}
