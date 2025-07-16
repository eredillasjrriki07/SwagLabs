using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SwagLabs.Mvc.Controllers;
using SwagLabs.Mvc.TagHelpers.Base;
using SwagLabs.Services.Utilities;

namespace SwagLabs.Mvc.TagHelpers;

public class ItemDetailsTagHelper : ItemLinkTagHelperBase
{
    public ItemDetailsTagHelper(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
        : base(contextAccessor, urlHelperFactory)
    {
        ActionName = nameof(UsersController.DetailsAsync).RemoveAsyncSuffix();
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        BuildContent(output, "text-info", "Details", "info-circle");
    }

}
