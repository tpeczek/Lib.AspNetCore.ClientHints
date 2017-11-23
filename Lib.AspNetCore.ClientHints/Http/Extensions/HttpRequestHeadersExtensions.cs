using System;
using System.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Primitives;
using Lib.AspNetCore.ClientHints.Http.Headers;
using ClientHintsHeaderNames = Lib.AspNetCore.ClientHints.Http.Headers.HeaderNames;

namespace Lib.AspNetCore.ClientHints.Http.Extensions
{
    /// <summary>
    /// Extensions for getting request headers used for Client Hints.
    /// </summary>
    public static class HttpRequestHeadersExtensions
    {
        #region Fields
        private const string SAVE_DATA_HEADER_VALUE_KEY = "Lib.AspNetCore.ClientHints.Http.Headers.SaveDataHeaderValue";

        private readonly static NumberFormatInfo _devicePixelRatioNumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        #endregion

        #region Methods
        /// <summary>
        /// Gets the Width header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Width header value.</returns>
        public static int? GetWidth(this HttpRequest request)
        {
            int? width = null;

            StringValues headerValue = request.Headers[ClientHintsHeaderNames.Width];
            if (!StringValues.IsNullOrEmpty(headerValue))
            {
                int parsedHeaderValue;
                if (HeaderUtilities.TryParseNonNegativeInt32(headerValue[headerValue.Count -1], out parsedHeaderValue))
                {
                    width = parsedHeaderValue;
                }
            }

            return width;
        }

        /// <summary>
        /// Gets the Viewport-Width header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Viewport-Width header value.</returns>
        public static int? GetViewportWidth(this HttpRequest request)
        {
            int? viewportWidth = null;

            StringValues headerValue = request.Headers[ClientHintsHeaderNames.ViewportWidth];
            if (!StringValues.IsNullOrEmpty(headerValue))
            {
                int parsedHeaderValue;
                if (HeaderUtilities.TryParseNonNegativeInt32(headerValue[headerValue.Count - 1], out parsedHeaderValue))
                {
                    viewportWidth = parsedHeaderValue;
                }
            }

            return viewportWidth;
        }

        /// <summary>
        /// Gets the DPR header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The DPR header value.</returns>
        public static decimal? GetDevicePixelRatio(this HttpRequest request)
        {
            decimal? devicePixelRatio = null;

            StringValues headerValue = request.Headers[ClientHintsHeaderNames.DevicePixelRatio];
            if (!StringValues.IsNullOrEmpty(headerValue))
            {
                decimal parsedHeaderValue;
                if (Decimal.TryParse(headerValue[headerValue.Count - 1], NumberStyles.AllowDecimalPoint, _devicePixelRatioNumberFormatInfo, out parsedHeaderValue))
                {
                    devicePixelRatio = parsedHeaderValue;
                }
            }

            return devicePixelRatio;
        }

        /// <summary>
        /// Gets the Save-Data header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Save-Data header value.</returns>
        public static SaveDataHeaderValue GetSaveData(this HttpRequest request)
        {
            if (!request.HttpContext.Items.ContainsKey(SAVE_DATA_HEADER_VALUE_KEY))
            {
                StringValues headerValue = request.Headers[ClientHintsHeaderNames.SaveData];
                if (!StringValues.IsNullOrEmpty(headerValue) && (headerValue.Count == 1))
                {
                    string[] tokens = ((string)headerValue).Split(';');
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        tokens[i] = tokens[i].Trim();
                    }

                    request.HttpContext.Items[SAVE_DATA_HEADER_VALUE_KEY] = new SaveDataHeaderValue(tokens);
                }
            }

            return request.HttpContext.Items[SAVE_DATA_HEADER_VALUE_KEY] as SaveDataHeaderValue;
        }
        #endregion
    }
}
