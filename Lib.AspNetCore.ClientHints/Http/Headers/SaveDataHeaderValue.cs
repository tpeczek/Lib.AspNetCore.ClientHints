using System;
using System.Linq;
using System.Collections.Generic;

namespace Lib.AspNetCore.ClientHints.Http.Headers
{
    /// <summary>
    /// Represents value of Save-Data header.
    /// </summary>
    public class SaveDataHeaderValue
    {
        #region Fields
        /// <summary>
        /// The token which is used as a signal indicating explicit user opt-in into a reduced data usage mode on the client.
        /// </summary>
        public const string ON_TOKEN = "on";

        private bool? _on = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value indicating if client has explicitly opt-in into a reduced data usage mode.
        /// </summary>
        public bool On
        {
            get
            {
                if (!_on.HasValue)
                {
                    _on = Tokens.Contains(ON_TOKEN, StringComparer.InvariantCultureIgnoreCase);
                }

                return _on.Value;
            }
        }

        /// <summary>
        /// Gets the tokens send by client.
        /// </summary>
        public IReadOnlyCollection<string> Tokens { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiates a new <see cref="SaveDataHeaderValue"/>.
        /// </summary>
        /// <param name="tokens">The tokens send by client</param>
        public SaveDataHeaderValue(IReadOnlyCollection<string> tokens)
        {
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
        }
        #endregion
    }
}
