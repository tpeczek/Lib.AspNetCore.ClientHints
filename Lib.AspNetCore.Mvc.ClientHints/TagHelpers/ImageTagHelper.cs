using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Lib.AspNetCore.ClientHints.Http.Extensions;

namespace Lib.AspNetCore.Mvc.ClientHints.TagHelpers
{
    /// <summary>
    /// <see cref="ITagHelper"/> implementation targeting &lt;img&gt; elements that supports alternative source for clients which has send a hint for reduced data usage.
    /// </summary>
    [HtmlTargetElement("img", Attributes = SRC_ATTRIBUTE_NAME + "," + SAVEDATA_SRC_ATTRIBUTE_NAME, TagStructure = TagStructure.WithoutEndTag)]
    public class ImageTagHelper : UrlResolutionTagHelper
    {
        #region Fields
        private const string SRC_ATTRIBUTE_NAME = "src";
        private const string SAVEDATA_SRC_ATTRIBUTE_NAME = "asp-savedata-src";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the reduced data source of the image.
        /// </summary>
        [HtmlAttributeName(SAVEDATA_SRC_ATTRIBUTE_NAME)]
        public string SaveDataSrc { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiates a new <see cref="ImageTagHelper"/>.
        /// </summary>
        /// <param name="urlHelperFactory">The <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="htmlEncoder">The <see cref="HtmlEncoder"/> to use.</param>
        public ImageTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder)
            : base(urlHelperFactory, htmlEncoder)
        { }
        #endregion

        #region Methods
        /// <summary>
        /// Processes the TagHelper.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="output">The output.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            
            output.CopyHtmlAttribute(SRC_ATTRIBUTE_NAME, context);
            if (ViewContext.HttpContext.Request.GetSaveData()?.On ?? false)
            {
                output.Attributes.SetAttribute(SRC_ATTRIBUTE_NAME, SaveDataSrc);
            }
            ProcessUrlAttribute(SRC_ATTRIBUTE_NAME, output);

            output.Attributes.RemoveAll(SAVEDATA_SRC_ATTRIBUTE_NAME);
        }
        #endregion
    }
}
