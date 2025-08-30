using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUglify.Helpers;

namespace SwagLabs.Web.TagHelpers.Base;

public abstract class ItemLinkTagHelperBase : TagHelper
{
    protected readonly IUrlHelper UrlHelper;

    public string DisplayText { get; set; }

    public int? ProductId { get; set; }

    public string? ClassName { get; set; }

    private readonly string _pageName;

    protected string ActionName { get; set; }

    protected ItemLinkTagHelperBase(IActionContextAccessor contextAccessor, IUrlHelperFactory urlHelperFactory)
    {
        UrlHelper = urlHelperFactory.GetUrlHelper(contextAccessor.ActionContext);
        _pageName = contextAccessor.ActionContext.ActionDescriptor.RouteValues["page"]?.Split("/", StringSplitOptions.RemoveEmptyEntries)[0];
    }

    protected void BuildContent(TagHelperOutput output)
    {
        output.TagName = "a";
        
        var target = (ProductId.HasValue) ? UrlHelper.Page($"/{_pageName}/{ActionName}", new { id = ProductId }) : UrlHelper.Page($"/{_pageName}/{ActionName}");
        
        output.Attributes.SetAttribute("href", target);

        if (!string.IsNullOrEmpty(ClassName))
        {
            output.Attributes.Add("class", $"text-decoration-none text-dark {ClassName}");
        }

        output.Content.AppendHtml(DisplayText);
    }

}