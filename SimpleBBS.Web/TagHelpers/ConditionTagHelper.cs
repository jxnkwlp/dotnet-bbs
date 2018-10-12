using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SimpleBBS.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = nameof(Condition))]
    public class ConditionTagHelper : TagHelper
    {
        /// <summary>
        ///  if set false, will not display
        /// </summary>
        [HtmlAttributeName("condition")]
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }
        }
    }
}
