using System;
using Lib.AspNetCore.ClientHints.Http.Headers;

namespace Lib.AspNetCore.ClientHints
{
    /// <summary>
    /// Options for the <see cref="ClientHintsMiddleware"/> middleware.
    /// </summary>
    public class ClientHintsOptions
    {
        #region Fields
        private int? _lifetime = null;
        #endregion

        #region Properties
        internal AcceptClientHintsHeaderValue AcceptClientHints { get; } = new AcceptClientHintsHeaderValue();

        /// <summary>
        /// Gets or sets the value indicating if Device Pixel Ratio hint is supported.
        /// </summary>
        public bool AcceptDevicePixelRatio
        {
            get { return AcceptClientHints.AcceptDevicePixelRatio; }

            set { AcceptClientHints.AcceptDevicePixelRatio = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if width hint is supported.
        /// </summary>
        public bool AcceptWidth
        {
            get { return AcceptClientHints.AcceptWidth; }

            set { AcceptClientHints.AcceptWidth = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if layout viewport width hint is supported.
        /// </summary>
        public bool AcceptViewportWidth
        {
            get { return AcceptClientHints.AcceptViewportWidth; }

            set { AcceptClientHints.AcceptViewportWidth = value; }
        }

        /// <summary>
        /// Gets or sets the value indicating if reduced data usage hint is supported.
        /// </summary>
        public bool AcceptSaveData
        {
            get { return AcceptClientHints.AcceptSaveData; }

            set { AcceptClientHints.AcceptSaveData = value; }
        }

        /// <summary>
        /// Gets or sets time (in seconds) for which a user agent can persist the advertised support.
        /// </summary>
        public int? Lifetime
        {
            get { return _lifetime; }

            set
            {
                if (value.HasValue && (value.Value < 0))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater or equal to zero.");
                }

                _lifetime = value;
            }
        }
        #endregion
    }
}
