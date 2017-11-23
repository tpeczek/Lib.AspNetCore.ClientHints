using System;
using Microsoft.Extensions.Options;
using Lib.AspNetCore.ClientHints;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for the <see cref="IApplicationBuilder"/> which can be used for adding <see cref="ClientHintsMiddleware"/> .
    /// </summary>
    public static class ClientHintsBuilderExtensions
    {
        /// <summary>
        /// Adds a <see cref="ClientHintsMiddleware"/> to application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to Configure method.</param>
        /// <returns>The original app parameter.</returns>
        public static IApplicationBuilder UseClientHints(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ClientHintsMiddleware>();
        }

        /// <summary>
        /// Adds a <see cref="ClientHintsMiddleware"/> to application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to Configure method.</param>
        /// <param name="options">An instance of the <see cref="ClientHintsOptions"/> to configure the middleware.</param>
        /// <returns>The original app parameter.</returns>
        public static IApplicationBuilder UseClientHints(this IApplicationBuilder app, ClientHintsOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<ClientHintsMiddleware>(Options.Create(options));
        }
    }
}
