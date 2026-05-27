namespace SpaceX.Infrastructure.Interfaces.Email;

public interface ITemplateProvider
{
    Task<string> GetTemplateAsync(string templateFor, string templateName, IReadOnlyDictionary<string, string?> parameters, CancellationToken cancellationToken = default);
}