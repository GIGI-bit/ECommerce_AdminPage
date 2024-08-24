using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ECommerce.WebUI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper: TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("az-filter")]
        public bool AZOrder { get; set; }
        [HtmlAttributeName("hl-filter")]
        public bool HighLowOrder { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            var sb = new StringBuilder();

            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");

                if (CurrentPage != 1)
                {
                    sb.Append("<li>");
                    sb.AppendFormat("<a  class='page-link' href='/product/index?page={0}&category={1}'> prev </a>", CurrentPage - 1, CurrentCategory);


                    sb.Append("</li>");
                }

                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                    sb.AppendFormat("<a  class='page-link' href='/product/index?page={0}&category={1}'> {2} </a>",
                        i, CurrentCategory, i);
                    sb.Append("</li>");
                }

                if (CurrentPage != PageCount)
                {
                    sb.Append("<li>");
                    sb.AppendFormat("<a  class='page-link' href='/product/index?page={0}&category={1}'> next </a>",CurrentPage+1,CurrentCategory);


                    sb.Append("</li>");
                }

                sb.Append("</ul>");
            }
        
        output.Content.SetHtmlContent(sb.ToString());
        }


    }
}
