using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace SimpleBBS.Web.TagHelpers
{
    [HtmlTargetElement("pager")]
    public class PagerTaghelper : TagHelper
    {
        [HtmlAttributeName("data")]
        public IPagedList Data { get; set; }

        public int ItemCount { get; set; } = 5;

        public string QueryName { get; set; } = "page";

        public PagerDisplayModel ShowFirstPage { get; set; }
        public PagerDisplayModel ShowLastPage { get; set; }

        public PagerDisplayModel ShowPreviousPage { get; set; }
        public PagerDisplayModel ShowNextPage { get; set; }


        public PagerDisplayModel ShowPagerModel { get; set; }

        public override int Order => 1;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        protected IUrlHelperFactory UrlHelperFactory;

        public PagerTaghelper(IUrlHelperFactory urlHelperFactory)
        {
            UrlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ItemCount <= 1)
                ItemCount = 1;

            if (Data != null)
            {
                var html = WritePagerBar();

                output.Content.SetHtmlContent(html);
            }

            if (output.Attributes.ContainsName("class"))
            {
                var old = output.Attributes["class"];
                output.Attributes.Remove(old);

                output.Attributes.Add("class", "pagerbar pagination " + old.Value);
            }
            else
            {
                output.Attributes.Add("class", "pagerbar pagination ");
            }
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";
        }



        private IHtmlContent WritePagerBar()
        {
            HtmlContentBuilder builder = new HtmlContentBuilder();

            if (ShowPagerModel == PagerDisplayModel.Never || (ShowFirstPage == PagerDisplayModel.Auto && Data.TotalPage == 1))
            {
                return builder;
            }

            if (ShowFirstPage == PagerDisplayModel.Always || (ShowFirstPage == PagerDisplayModel.Auto && Data.TotalPage > 1))
                builder.AppendHtml(WriteItem("首页", 1, HasFirst(Data), false));

            if (ShowPreviousPage == PagerDisplayModel.Always || (ShowPreviousPage == PagerDisplayModel.Auto && Data.TotalPage > 1))
                builder.AppendHtml(WriteItem("上一页", Data.PageNumber - 1, HasPrevious(Data), false));

            if (HasPreviousMore(Data, ItemCount))
            {
                builder.AppendHtml(WriteItem("...", Data.PageNumber - 1, false, false));
            }

            var start = GetStartNumber(Data);
            var end = GetEndNumber(Data);

            for (int i = start; i <= end; i++)
            {
                builder.AppendHtml(WriteItem(i.ToString(), i, Data.PageNumber != i, Data.PageNumber == i));
            }

            if (HasNextMore(Data, ItemCount))
            {
                builder.AppendHtml(WriteItem("...", Data.PageNumber + 1, false, false));
            }

            if (ShowNextPage == PagerDisplayModel.Always || (ShowNextPage == PagerDisplayModel.Auto && Data.TotalPage > 1))
                builder.AppendHtml(WriteItem("下一页", Data.PageNumber + 1, HasNext(Data), false));

            if (ShowLastPage == PagerDisplayModel.Always || (ShowLastPage == PagerDisplayModel.Auto && Data.TotalPage > 1))
                builder.AppendHtml(WriteItem("末页", Data.TotalPage, HasLast(Data), false));

            return builder;
        }



        private TagBuilder WriteItem(string text, int page, bool hasLink, bool active)
        {
            TagBuilder tagBuilder = new TagBuilder("li");

            tagBuilder.AddCssClass("pager-item");

            if (hasLink)
            {
                var link = new TagBuilder("a");
                link.Attributes["href"] = BuilderLink(page);
                link.InnerHtml.SetContent(text);
                tagBuilder.InnerHtml.AppendHtml(link);
            }
            else
            {
                var link = new TagBuilder("span");
                link.InnerHtml.SetContent(text);
                tagBuilder.InnerHtml.AppendHtml(link);
            }

            if (active)
            {
                tagBuilder.AddCssClass("active");
            }

            return tagBuilder;
        }


        RouteValueDictionary GetQueryStrings(int pageNumber)
        {
            var routeValues = new RouteValueDictionary(); //_html.ViewContext.RouteData.Values;
            var queryStrings = ViewContext.HttpContext.Request.Query;

            foreach (var key in queryStrings.Keys.Where(key => key != null))
            {
                var value = queryStrings[key];
                routeValues[key] = value;
            }

            if (pageNumber == 1)
            {
                if (routeValues.ContainsKey(QueryName))
                    routeValues.Remove(QueryName);
            }
            else
            {
                routeValues[QueryName] = pageNumber;
            }

            return routeValues;
        }

        string BuilderLink(int page)
        {
            var query = GetQueryStrings(page);

            var urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);

            var queryString = string.Join("&", query.Select(t => t.Key + "=" + t.Value));

            return ViewContext.HttpContext.Request.Path + "?" + queryString;
        }


        static bool HasFirst(IPagedList pagedList)
        {
            return pagedList.PageNumber > 1;
        }

        static bool HasPrevious(IPagedList pagedList)
        {
            return pagedList.PageNumber > 1;
        }

        static bool HasNext(IPagedList pagedList)
        {
            return pagedList.PageNumber < pagedList.TotalPage;
        }

        static bool HasLast(IPagedList pagedList)
        {
            return pagedList.PageNumber < pagedList.TotalPage;
        }

        static int GetPreviousNumber(IPagedList pagedList)
        {
            var i = pagedList.PageNumber - 1;

            return i <= 1 ? 1 : i;
        }

        static int GetNextNumber(IPagedList pagedList)
        {
            var i = pagedList.PageNumber + 1;

            return i >= pagedList.TotalPage ? pagedList.TotalPage : i;
        }


        static int GetStartNumber(IPagedList pagedList, int itemCount = 5)
        {
            var s = itemCount / 2;

            var index = pagedList.PageNumber - s;

            if (pagedList.TotalPage - itemCount < index)
                index = pagedList.TotalPage - itemCount + 1;

            if (index <= 1)
                index = 1;

            return index;
        }

        static int GetEndNumber(IPagedList pagedList, int itemCount = 5)
        {
            var s = itemCount / 2;

            var index = pagedList.PageNumber + s;

            if (index <= itemCount)
                index = itemCount;

            if (index >= pagedList.TotalPage)
                index = pagedList.TotalPage;

            return index;
        }


        static bool HasNextMore(IPagedList pagedList, int itemCount = 5)
        {
            var end = GetEndNumber(pagedList, itemCount);

            return (end < pagedList.TotalPage);
        }

        static bool HasPreviousMore(IPagedList pagedList, int itemCount = 5)
        {
            var start = GetStartNumber(pagedList, itemCount);

            return (start > 1);
        }



    }

    public enum PagerDisplayModel
    {
        Auto,
        Always,
        Never,
    }
}
