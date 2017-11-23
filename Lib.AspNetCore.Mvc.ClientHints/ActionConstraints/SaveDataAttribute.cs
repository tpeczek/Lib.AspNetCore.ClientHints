using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Lib.AspNetCore.ClientHints.Http.Extensions;

namespace Lib.AspNetCore.Mvc.ClientHints.ActionConstraints
{
    /// <summary>
    /// Attribute which enables or disables an action for a given request if client has send a hint for reduced data usage. See <see cref="IActionConstraint"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SaveDataAttribute : Attribute, IActionConstraint
    {
        #region Fields
        private bool _on;
        #endregion

        #region Properties
        /// <summary>
        /// Get or sets the constraint order.
        /// </summary>
        public int Order { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes new instance of <see cref="SaveDataAttribute"/>
        /// </summary>
        /// <param name="on">The value indicating if an action should be enabled when the hint for reduced data usage is send.</param>
        public SaveDataAttribute(bool on)
        {
            _on = on;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether an action is a valid candidate for selection.
        /// </summary>
        /// <param name="context">The <see cref="ActionConstraintContext"/>.</param>
        /// <returns>True if the action is valid for selection, otherwise false.</returns>
        public bool Accept(ActionConstraintContext context)
        {
            return (context.RouteContext.HttpContext.Request.GetSaveData()?.On ?? false) == _on;
        }
        #endregion
    }
}
