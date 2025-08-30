using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SwagLabs.Web.TagHelpers.Base;

namespace SwagLabs.Web.TagHelpers;

public class ItemListTagHelper : ItemLinkTagHelperBase
{
    public ItemListTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = "Index";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //BuildContent(output, "text-default", "Back to List", "list");
    }
}
