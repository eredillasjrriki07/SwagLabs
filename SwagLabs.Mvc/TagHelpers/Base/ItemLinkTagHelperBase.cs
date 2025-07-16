using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SwagLabs.Mvc.TagHelpers.Base;

public class ItemLinkTagHelperBase : TagHelper
{
    protected readonly IUrlHelper UrlHelper;

    private readonly string _controllerName;

    protected string ActionName { get; set; }

    public int? ItemId { get; set; }

    public ItemLinkTagHelperBase(IActionContextAccessor contextAccessor, IUrlHelperFactory urlhHelperFactory)
    {
        UrlHelper = urlhHelperFactory.GetUrlHelper(contextAccessor.ActionContext);
        _controllerName = contextAccessor.ActionContext.ActionDescriptor.RouteValues["controller"];
    }

    protected void BuildContent(TagHelperOutput output, string cssClassName, string displayText, string fontAwesomeName)
    {
        output.TagName = "a";
        var target = (ItemId.HasValue) ? UrlHelper.Action(ActionName, _controllerName, new { id = ItemId }) : UrlHelper.Action(ActionName, _controllerName);
        output.Attributes.SetAttribute("href", target);
        output.Attributes.SetAttribute("class", cssClassName);
        output.Content.AppendHtml($@"{displayText} <i class=""fas fa-{fontAwesomeName}""></i>");
    }
}
