using System.Text;

namespace Lib.AspNetCore.ClientHints.Http.Headers
{
    /// <summary>
    /// Represents value of Accept-CH header.
    /// </summary>
    public class AcceptClientHintsHeaderValue
    {
        #region Fields
        private bool _acceptDevicePixelRatio = false;
        private bool _acceptWidth = false;
        private bool _acceptViewportWidth = false;
        private bool _acceptSaveData = false;

        private string _headerValue = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if Device Pixel Ratio hint is supported.
        /// </summary>
        public bool AcceptDevicePixelRatio
        {
            get { return _acceptDevicePixelRatio; }

            set
            {
                _headerValue = null;
                _acceptDevicePixelRatio = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if width hint is supported.
        /// </summary>
        public bool AcceptWidth
        {
            get { return _acceptWidth; }

            set
            {
                _headerValue = null;
                _acceptWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if layout viewport width hint is supported.
        /// </summary>
        public bool AcceptViewportWidth
        {
            get { return _acceptViewportWidth; }

            set
            {
                _headerValue = null;
                _acceptViewportWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating if reduced data usage hint is supported.
        /// </summary>
        public bool AcceptSaveData
        {
            get { return _acceptSaveData; }

            set
            {
                _headerValue = null;
                _acceptSaveData = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the string representation of header value.
        /// </summary>
        /// <returns>The string representation of header value.</returns>
        public override string ToString()
        {
            if (_headerValue == null)
            {
                StringBuilder headerValueBuilder = new StringBuilder();

                if (_acceptDevicePixelRatio)
                {
                    headerValueBuilder.Append(HeaderNames.DevicePixelRatio).Append(", ");
                }

                if (_acceptWidth)
                {
                    headerValueBuilder.Append(HeaderNames.Width).Append(", ");
                }

                if (_acceptViewportWidth)
                {
                    headerValueBuilder.Append(HeaderNames.ViewportWidth).Append(", ");
                }

                if (_acceptSaveData)
                {
                    headerValueBuilder.Append(HeaderNames.SaveData).Append(", ");
                }

                if (headerValueBuilder.Length > 0)
                {
                    headerValueBuilder.Length -= 2;
                }

                _headerValue = headerValueBuilder.ToString();
            }

            return _headerValue;
        }
        #endregion
    }
}
