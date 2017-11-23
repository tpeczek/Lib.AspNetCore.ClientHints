using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Lib.AspNetCore.ClientHints.Http.Extensions;
using ClientHintsHeaderNames = Lib.AspNetCore.ClientHints.Http.Headers.HeaderNames;

namespace Lib.AspNetCore.ClientHints
{
    /// <summary>
    /// Middleware which allows application to advertise support for Client Hints.
    /// </summary>
    public class ClientHintsMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        private readonly ClientHintsOptions _options;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiates a new <see cref="ClientHintsMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">An instance of the <see cref="ClientHintsOptions"/> to configure the middleware.</param>
        public ClientHintsMiddleware(RequestDelegate next, IOptions<ClientHintsOptions> options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task Invoke(HttpContext context)
        {
            if (context.Request.IsHttps)
            {
                context.Response.SetAcceptClientHints(_options.AcceptClientHints);
                if (_options.Lifetime.HasValue)
                {
                    context.Response.SetAcceptClientHintsLifetime(_options.Lifetime.Value);
                }
            }

            SetVaryHeader(context);

            return _next(context);
        }

        private void SetVaryHeader(HttpContext context)
        {
            bool varyByDevicePixelRatio = _options.AcceptDevicePixelRatio;
            bool varyByWidth = _options.AcceptWidth;
            bool varyByViewportWidth = _options.AcceptViewportWidth;
            bool varyBySaveData = _options.AcceptSaveData;

            string[] varyValues = context.Response.Headers.GetCommaSeparatedValues(HeaderNames.Vary);

            for (int i = 0; i < varyValues.Length; i++)
            {
                if (varyByDevicePixelRatio && String.Equals(varyValues[i], ClientHintsHeaderNames.DevicePixelRatio, StringComparison.OrdinalIgnoreCase))
                {
                    varyByDevicePixelRatio = false;
                }

                if (varyByWidth && String.Equals(varyValues[i], ClientHintsHeaderNames.Width, StringComparison.OrdinalIgnoreCase))
                {
                    varyByWidth = false;
                }

                if (varyByViewportWidth && String.Equals(varyValues[i], ClientHintsHeaderNames.ViewportWidth, StringComparison.OrdinalIgnoreCase))
                {
                    varyByViewportWidth = false;
                }

                if (varyBySaveData && String.Equals(varyValues[i], ClientHintsHeaderNames.SaveData, StringComparison.OrdinalIgnoreCase))
                {
                    varyBySaveData = false;
                }

                if (!varyByDevicePixelRatio && !varyByWidth && !varyByViewportWidth && !varyBySaveData)
                {
                    break;
                }
            }

            if (varyByDevicePixelRatio)
            {
                context.Response.Headers.AppendCommaSeparatedValues(HeaderNames.Vary, ClientHintsHeaderNames.DevicePixelRatio);
            }

            if (varyByWidth)
            {
                context.Response.Headers.AppendCommaSeparatedValues(HeaderNames.Vary, ClientHintsHeaderNames.Width);
            }

            if (varyByViewportWidth)
            {
                context.Response.Headers.AppendCommaSeparatedValues(HeaderNames.Vary, ClientHintsHeaderNames.ViewportWidth);
            }

            if (varyBySaveData)
            {
                context.Response.Headers.AppendCommaSeparatedValues(HeaderNames.Vary, ClientHintsHeaderNames.SaveData);
            }
        }
        #endregion
    }
}
