using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SwagLabs.Mvc.Controllers;
using SwagLabs.Mvc.TagHelpers.Base;
using SwagLabs.Services.Utilities;

namespace SwagLabs.Mvc.TagHelpers;

public class ItemListTagHelper : ItemLinkTagHelperBase
{
    public ItemListTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(UsersController.IndexAsync).RemoveAsyncSuffix();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "text-default", "Back to List", "list");
    }
}
