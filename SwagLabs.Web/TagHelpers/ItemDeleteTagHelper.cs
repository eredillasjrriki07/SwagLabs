using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SwagLabs.Web.TagHelpers.Base;

namespace SwagLabs.Web.TagHelpers;

public class ItemDeleteTagHelper : ItemLinkTagHelperBase
{
    public ItemDeleteTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = "Delete";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //BuildContent(output, "text-danger", "Delete", "trash");
    }
}
