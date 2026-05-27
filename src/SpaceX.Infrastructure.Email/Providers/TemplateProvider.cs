using SpaceX.Infrastructure.Email.Constants;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Infrastructure.Email.Providers;

public class TemplateProvider : ITemplateProvider
{
    public async Task<string> GetTemplateAsync(string templateFor, string templateName, IReadOnlyDictionary<string, string?> parameters, CancellationToken cancellationToken = default)
    {
        var rawHtml = await GetTemplateAsync(templateFor, templateName, cancellationToken);

        return ParseHtmlWithParameters(rawHtml, parameters);
    }

    private static async Task<string> GetTemplateAsync(string templateFor, string templateName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(templateFor);
        ArgumentException.ThrowIfNullOrWhiteSpace(templateName);

        var templatePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            TemplateFolderConstants.BaseFolder,
            templateFor,
            templateName);

        return await File.ReadAllTextAsync(templatePath, cancellationToken);
    }

    private static string ParseHtmlWithParameters(string rawHtml, IReadOnlyDictionary<string, string?> parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rawHtml);

        if (parameters.Count == 0)
        {
            return rawHtml;
        }

        foreach (var parameter in parameters)
        {
            rawHtml = rawHtml.Replace(parameter.Key, parameter.Value ?? string.Empty);
        }

        return rawHtml;
    }
}