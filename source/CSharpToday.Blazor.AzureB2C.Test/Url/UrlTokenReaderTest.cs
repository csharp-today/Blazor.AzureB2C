using CSharpToday.Blazor.AzureB2C.Url;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace CSharpToday.Blazor.AzureB2C.Test.Url
{
    [TestClass]
    public class UrlTokenReaderTest : BaseTest
    {
        [TestMethod]
        public void Get_Broken_Token() => LucidTest
            .DefineExpected(new
            {
                Error = "expected-error",
                Description = "Error description !@#$"
            })
            .Arrange(expected => $"my-url{UrlTokenReader.ErrorNamePrefix}{Uri.EscapeDataString(expected.Error)}{UrlTokenReader.ErrorDescriptionPrefix}{Uri.EscapeDataString(expected.Description)}")
            .Act(GetToken)
            .Assert((expected, token) =>
            {
                var brokenToken = token as BrokenUrlToken;
                brokenToken.ShouldNotBeNull();
                brokenToken.Error.ShouldBe(expected.Error);
                brokenToken.ErrorDescription.ShouldBe(expected.Description);
            });

        [TestMethod]
        public void Get_Token() => LucidTest
            .DefineExpected("expected-token")
            .Arrange(expectedToken => $"my-url{UrlTokenReader.AuthTokenPrefix}{expectedToken}")
            .Act(GetToken)
            .Assert((expectedToken, token) =>
            {
                var validToken = token as ValidUrlToken;
                validToken.ShouldNotBeNull("Token is not valid");
                validToken.RawToken.ShouldBe(expectedToken);
            });

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("url-without-token")]
        public void Return_Null_When_No_Token(string url) => LucidTest
            .Act(() => GetToken(url))
            .Assert(token => token.ShouldBeNull());

        private UrlToken GetToken(string url) => Get<UrlTokenReader>().GetTokenFromUrl(url);
    }
}
