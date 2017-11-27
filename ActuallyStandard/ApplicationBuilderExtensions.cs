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
        /// </summary>
        public static IApplicationBuilder UseSecurityHttpHeaders(this IApplicationBuilder application) =>
            application
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
