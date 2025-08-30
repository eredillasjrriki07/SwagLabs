using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SwagLabs.Web.TagHelpers.Base;

namespace SwagLabs.Web.TagHelpers;

public class ItemCreateTagHelper : ItemLinkTagHelperBase
{
    public ItemCreateTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = "Create";
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //BuildContent(output, "text-success", "Create", "plus");
    }
}
