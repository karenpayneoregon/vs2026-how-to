using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebClassLibrary.TagHelpers;

/// <summary>
/// A custom <see cref="TagHelper"/> that renders a label indicating the current hosting environment.
/// </summary>
/// <remarks>
/// This tag helper targets the <c>environment-label</c> HTML element and generates a styled
/// <c>span</c> element displaying the name of the current environment. The styling can be customized
/// using the <see cref="Class"/> property or defaults to a CSS class based on the environment name.
/// </remarks>
[HtmlTargetElement("environment-label")]
public class EnvironmentLabelTagHelper(IWebHostEnvironment environment) : TagHelper
{
    public string? Class { get; set; }

    /// <summary>
    /// Processes the <c>environment-label</c> tag helper to generate a <c>span</c> element
    /// that displays the current environment name with appropriate CSS styling.
    /// </summary>
    /// <param name="context">
    /// The <see cref="TagHelperContext"/> containing information associated with the current HTML tag.
    /// </param>
    /// <param name="output">
    /// The <see cref="TagHelperOutput"/> used to modify the output of the HTML tag.
    /// </param>
    /// <remarks>
    /// This method sets the <c>class</c> attribute of the <c>span</c> element based on the
    /// <see cref="Class"/> property or a default CSS class derived from the environment name.
    /// It also sets the <c>title</c> attribute to indicate the current environment and
    /// populates the content of the <c>span</c> element with the environment name.
    /// </remarks>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var environmentName = environment.EnvironmentName;

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var cssClass = string.IsNullOrWhiteSpace(Class)
            ? GetDefaultCssClass(environmentName)
            : Class;

        output.Attributes.SetAttribute("class", cssClass);
        output.Attributes.SetAttribute("title", $"Current environment: {environmentName}");

        output.Content.SetContent(environmentName);
        
    }

    /// <summary>
    /// Determines the default CSS class for the environment label based on the specified environment name.
    /// </summary>
    /// <param name="environmentName">The name of the environment (e.g., "Development", "Staging", "Production").</param>
    /// <returns>
    /// A <see cref="string"/> representing the CSS class to be applied to the environment label.
    /// </returns>
    /// <remarks>
    /// The method uses a switch expression to map environment names to their corresponding CSS classes.
    /// If the environment name does not match any predefined values, a default CSS class is returned.
    /// </remarks>
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