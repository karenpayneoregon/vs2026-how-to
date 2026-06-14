using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FluentWebApplication.TagHelpers;

[HtmlTargetElement("environment-label")]
public class EnvironmentLabelTagHelper : TagHelper
{
    private readonly IWebHostEnvironment _environment;

    public EnvironmentLabelTagHelper(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public string? Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var environmentName = _environment.EnvironmentName;

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var cssClass = string.IsNullOrWhiteSpace(Class)
            ? GetDefaultCssClass(environmentName)
            : Class;

        output.Attributes.SetAttribute("class", cssClass);
        output.Attributes.SetAttribute("title", $"Current environment: {environmentName}");

        output.Content.SetContent(environmentName);
    }

    private static string GetDefaultCssClass(string environmentName)
    {
        return environmentName switch
        {
            "Development" => "badge bg-success",
            "Staging" => "badge bg-warning text-dark",
            "Production" => "badge bg-danger",
            _ => "badge bg-secondary"
        };
    }
}