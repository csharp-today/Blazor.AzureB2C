using System;

namespace CSharpToday.Blazor.AzureB2C.Url
{
    internal class UrlTokenReader : IUrlTokenReader
    {
        internal const string AuthTokenPrefix = "#id_token=";
        internal const string ErrorNamePrefix = "#error=";
        internal const string ErrorDescriptionPrefix = "&error_description=";

        public UrlToken GetTokenFromUrl(string url)
        {
            var idx = url?.IndexOf(AuthTokenPrefix) ?? -1;
            if (idx >= 0)
            {
                return new ValidUrlToken(url.Substring(idx + AuthTokenPrefix.Length));
            }

            idx = url?.IndexOf(ErrorNamePrefix) ?? -1;
            if (idx >= 0)
            {
                idx += ErrorNamePrefix.Length;
                int descriptionIdx = url.IndexOf(ErrorDescriptionPrefix);
                var error = descriptionIdx > 0
                    ? url.Substring(idx, descriptionIdx - idx)
                    : url.Substring(idx);
                var description = descriptionIdx > 0
                    ? url.Substring(descriptionIdx + ErrorDescriptionPrefix.Length)
                    : null;
                return new BrokenUrlToken(
                    Uri.UnescapeDataString(error),
                    Uri.UnescapeDataString(description));
            }

            return null;
        }
    }
}
