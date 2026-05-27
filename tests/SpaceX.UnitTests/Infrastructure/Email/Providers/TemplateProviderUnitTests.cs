using SpaceX.Infrastructure.Email.Constants;
using SpaceX.Infrastructure.Email.Providers;

namespace SpaceX.UnitTests.Infrastructure.Email.Providers;

public class TemplateProviderTests : IDisposable
{
    private readonly TemplateProvider _sut = new();

    private readonly string _templateFor = "TestTemplates";
    private readonly string _templateName = "test-template.html";
    private readonly string _templateDirectory;
    private readonly string _templatePath;

    public TemplateProviderTests()
    {
        _templateDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            TemplateFolderConstants.BaseFolder,
            _templateFor);

        _templatePath = Path.Combine(_templateDirectory, _templateName);

        Directory.CreateDirectory(_templateDirectory);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateExists_ReturnsTemplateContent()
    {
        // Arrange
        const string rawHtml = "<html><body>Hello Vlatko Petrushevski</body></html>";

        var parameters = new Dictionary<string, string?>();

        await File.WriteAllTextAsync(_templatePath, rawHtml);

        // Act
        var result = await _sut.GetTemplateAsync(
            _templateFor,
            _templateName,
            parameters,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rawHtml, result);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateForIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        string? templateFor = null;

        var parameters = new Dictionary<string, string?>();

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.GetTemplateAsync(
                templateFor!,
                _templateName,
                parameters,
                CancellationToken.None));

        // Assert
        Assert.Equal("templateFor", exception.ParamName);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateForIsWhiteSpace_ThrowsArgumentException()
    {
        // Arrange
        const string templateFor = " ";

        var parameters = new Dictionary<string, string?>();

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.GetTemplateAsync(
                templateFor,
                _templateName,
                parameters,
                CancellationToken.None));

        // Assert
        Assert.Equal("templateFor", exception.ParamName);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateNameIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        string? templateName = null;

        var parameters = new Dictionary<string, string?>();

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.GetTemplateAsync(
                _templateFor,
                templateName!,
                parameters,
                CancellationToken.None));

        // Assert
        Assert.Equal("templateName", exception.ParamName);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateNameIsWhiteSpace_ThrowsArgumentException()
    {
        // Arrange
        const string templateName = " ";

        var parameters = new Dictionary<string, string?>();

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.GetTemplateAsync(
                _templateFor,
                templateName,
                parameters,
                CancellationToken.None));

        // Assert
        Assert.Equal("templateName", exception.ParamName);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenTemplateDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        const string templateName = "missing-template.html";

        var parameters = new Dictionary<string, string?>();

        // Act
        var exception = await Assert.ThrowsAsync<FileNotFoundException>(
            () => _sut.GetTemplateAsync(
                _templateFor,
                templateName,
                parameters,
                CancellationToken.None));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenParametersAreProvided_ReplacesParameters()
    {
        // Arrange
        const string rawHtml = "<html><body>Hello {{FirstName}} {{LastName}}, email: {{Email}}</body></html>";
        const string expectedHtml = "<html><body>Hello Vlatko Petrushevski, email: test@test.com</body></html>";

        var parameters = new Dictionary<string, string?>
        {
            { "{{FirstName}}", "Vlatko" },
            { "{{LastName}}", "Petrushevski" },
            { "{{Email}}", "test@test.com" }
        };

        await File.WriteAllTextAsync(_templatePath, rawHtml);

        // Act
        var result = await _sut.GetTemplateAsync(
            _templateFor,
            _templateName,
            parameters,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedHtml, result);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenParameterValueIsNull_ReplacesParameterWithEmptyString()
    {
        // Arrange
        const string rawHtml = "<html><body>Hello {{FirstName}} {{LastName}}</body></html>";
        const string expectedHtml = "<html><body>Hello Vlatko </body></html>";

        var parameters = new Dictionary<string, string?>
        {
            { "{{FirstName}}", "Vlatko" },
            { "{{LastName}}", null }
        };

        await File.WriteAllTextAsync(_templatePath, rawHtml);

        // Act
        var result = await _sut.GetTemplateAsync(
            _templateFor,
            _templateName,
            parameters,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedHtml, result);
    }

    [Fact]
    public async Task GetTemplateAsync_WhenParametersAreEmpty_ReturnsRawHtml()
    {
        // Arrange
        const string rawHtml = "<html><body>Hello {{FirstName}}</body></html>";

        var parameters = new Dictionary<string, string?>();

        await File.WriteAllTextAsync(_templatePath, rawHtml);

        // Act
        var result = await _sut.GetTemplateAsync(
            _templateFor,
            _templateName,
            parameters,
            CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rawHtml, result);
    }

    public void Dispose()
    {
        var templateRoot = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            TemplateFolderConstants.BaseFolder,
            _templateFor);

        if (Directory.Exists(templateRoot))
        {
            Directory.Delete(templateRoot, true);
        }
    }
}