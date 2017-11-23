using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Lib.AspNetCore.ClientHints.Http.Headers;

namespace Lib.AspNetCore.ClientHints.Http.Extensions
{
    /// <summary>
    /// Extensions for setting response headers used for Client Hints.
    /// </summary>
    public static class HttpResponseHeadersExtensions
    {
        #region Fields
        private readonly static NumberFormatInfo _devicePixelRatioNumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        #endregion

        #region Methods
        /// <summary>
        /// Sets the Accept-CH header value.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="acceptClientHints">The Accept-CH header value.</param>
        public static void SetAcceptClientHints(this HttpResponse response, AcceptClientHintsHeaderValue acceptClientHints)
        {
            response.SetResponseHeader(HeaderNames.AcceptClientHints, acceptClientHints.ToString());
        }

        /// <summary>
        /// Sets the Accept-CH-Lifetime header value.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="lifetime">The Accept-CH-Lifetime header value.</param>
        public static void SetAcceptClientHintsLifetime(this HttpResponse response, int lifetime)
        {
            if (lifetime < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lifetime), "Value must be greater or equal to zero.");
            }

            response.SetResponseHeader(HeaderNames.AcceptClientHintsLifetime, lifetime.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets the Content-DPR header value.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="contentDevicePixelRatio">The Content-DPR header value.</param>
        public static void SetContentDevicePixelRatio(this HttpResponse response, decimal contentDevicePixelRatio)
        {
            if (contentDevicePixelRatio <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contentDevicePixelRatio), "Value must be greater or equal to zero.");
            }

            response.SetResponseHeader(HeaderNames.ContentDevicePixelRatio, contentDevicePixelRatio.ToString(_devicePixelRatioNumberFormatInfo));
        }

        internal static void SetResponseHeader(this HttpResponse response, string headerName, string headerValue)
        {
            if (String.IsNullOrWhiteSpace(headerValue))
            {
                response.Headers.Remove(headerName);
            }
            else
            {
                response.Headers[headerName] = headerValue;
            }
        }
        #endregion
    }
}
