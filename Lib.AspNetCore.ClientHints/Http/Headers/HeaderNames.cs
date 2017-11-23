namespace Lib.AspNetCore.ClientHints.Http.Headers
{
    /// <summary>
    /// The names of HTTP headers used for Client Hints.
    /// </summary>
    public static class HeaderNames
    {
        /// <summary>
        /// The Accept-CH header.
        /// </summary>
        public const string AcceptClientHints = "Accept-CH";

        /// <summary>
        /// The Accept-CH-Lifetime header.
        /// </summary>
        public const string AcceptClientHintsLifetime = "Accept-CH-Lifetime";

        /// <summary>
        /// The DPR header.
        /// </summary>
        public const string DevicePixelRatio = "DPR";

        /// <summary>
        /// The Content-DPR header.
        /// </summary>
        public const string ContentDevicePixelRatio = "Content-DPR";

        /// <summary>
        /// The Width header.
        /// </summary>
        public const string Width = "Width";

        /// <summary>
        /// The Viewport-Width header.
        /// </summary>
        public const string ViewportWidth = "Viewport-Width";

        /// <summary>
        /// The Save-Data header.
        /// </summary>
        public const string SaveData = "Save-Data";
    }
}
