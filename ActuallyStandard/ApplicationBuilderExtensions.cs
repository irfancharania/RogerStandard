using Microsoft.AspNetCore.Builder;

namespace ActuallyStandard
{
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// https://github.com/NWebsec/NWebsec/wiki
        /// https://dotnetcoretutorials.com/2017/01/20/set-x-content-type-options-asp-net-core/
        /// https://dotnetcoretutorials.com/2017/01/10/set-x-xss-protection-asp-net-core/
        /// https://dotnetcoretutorials.com/2017/01/08/set-x-frame-options-asp-net-core/
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
        /// http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
        /// Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
        /// Note: You can refer to the following article to clear the HSTS cache in your browser:
        /// http://classically.me/blogs/how-clear-hsts-settings-major-browsers
        /// </summary>
        public static IApplicationBuilder UseSecurityHttpHeaders(this IApplicationBuilder application) =>
            application
                // Adds the Strict-Transport-Security HTTP header to responses. This HTTP header is only relevant if you are
                // using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate
                // errors and warnings.
                .UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload())
                // X-Content-Type-Options - Adds the X-Content-Type-Options HTTP header. Stop IE9 and below from
                //                          sniffing files and overriding the Content-Type header (MIME type).
                .UseXContentTypeOptions()
                // X-Download-Options - Adds the X-Download-Options HTTP header. When users save the page, stops them
                //                      from opening it and forces a save and manual open.
                .UseXDownloadOptions()
                // X-Frame-Options - Adds the X-Frame-Options HTTP header. Stop clickjacking by stopping the page from
                //                   opening in an iframe or only allowing it from the same origin.
                //   SameOrigin - Specifies that the X-Frame-Options header should be set in the HTTP response,
                //                instructing the browser to display the page when it is loaded in an iframe - but only
                //                if the iframe is from the same origin as the page.
                //   Deny - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing
                //          the browser to not display the page when it is loaded in an iframe.
                .UseXfo(options => options.Deny())
                // Enables XSS protection and if XSS is detected, the browser stops rendering altogether.
                .UseXXssProtection(options => options.EnabledWithBlockMode())
            ;
    }
}
