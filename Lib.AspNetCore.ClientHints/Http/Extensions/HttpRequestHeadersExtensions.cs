using System;
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

        private readonly static NumberFormatInfo _clientHintNumberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        #endregion

        #region Methods
        /// <summary>
        /// Gets the Width header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Width header value.</returns>
        public static int? GetWidth(this HttpRequest request)
        {
            return request.GetNullableNonNegativeInt32HeaderValue(ClientHintsHeaderNames.Width);
        }

        /// <summary>
        /// Gets the Viewport-Width header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Viewport-Width header value.</returns>
        public static int? GetViewportWidth(this HttpRequest request)
        {
            return request.GetNullableNonNegativeInt32HeaderValue(ClientHintsHeaderNames.ViewportWidth);
        }

        /// <summary>
        /// Gets the Device-Memory header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The Device-Memory header value.</returns>
        public static decimal? GetDeviceMemory(this HttpRequest request)
        {
            return request.GetNullableDecimalHeaderValue(ClientHintsHeaderNames.DeviceMemory);
        }

        /// <summary>
        /// Gets the DPR header value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The DPR header value.</returns>
        public static decimal? GetDevicePixelRatio(this HttpRequest request)
        {
            return request.GetNullableDecimalHeaderValue(ClientHintsHeaderNames.DevicePixelRatio);
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

        private static int? GetNullableNonNegativeInt32HeaderValue(this HttpRequest request, string headerName)
        {
            int? headerValue = null;

            StringValues rawHeaderValues = request.Headers[headerName];
            if (!StringValues.IsNullOrEmpty(rawHeaderValues))
            {
                int parsedHeaderValue;
                if (HeaderUtilities.TryParseNonNegativeInt32(rawHeaderValues[rawHeaderValues.Count - 1], out parsedHeaderValue))
                {
                    headerValue = parsedHeaderValue;
                }
            }

            return headerValue;
        }

        private static decimal? GetNullableDecimalHeaderValue(this HttpRequest request, string headerName)
        {
            decimal? headerValue = null;

            StringValues rawHeaderValues = request.Headers[headerName];
            if (!StringValues.IsNullOrEmpty(rawHeaderValues))
            {
                decimal parsedHeaderValue;
                if (Decimal.TryParse(rawHeaderValues[rawHeaderValues.Count - 1], NumberStyles.AllowDecimalPoint, _clientHintNumberFormatInfo, out parsedHeaderValue))
                {
                    headerValue = parsedHeaderValue;
                }
            }

            return headerValue;
        }
        #endregion
    }
}
