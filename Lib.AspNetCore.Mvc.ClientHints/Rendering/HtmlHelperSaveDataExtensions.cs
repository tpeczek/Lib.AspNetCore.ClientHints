using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lib.AspNetCore.ClientHints.Http.Extensions;

namespace Lib.AspNetCore.Mvc.ClientHints.Rendering
{
    /// <summary>
    /// Save-Data related extensions for <see cref="IHtmlHelper"/>.
    /// </summary>
    public static class HtmlHelperSaveDataExtensions
    {
        /// <summary>
        /// Gets the information indicating if client has send a hint for reduced data usage.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="IHtmlHelper"/> instance this method extends.</param>
        /// <returns>True if client has send a hint for reduced data usage, otherwise false.</returns>
        public static bool ShouldSaveData(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }
            
            return htmlHelper.ViewContext.HttpContext.Request.GetSaveData()?.On ?? false;
        }
    }
}
