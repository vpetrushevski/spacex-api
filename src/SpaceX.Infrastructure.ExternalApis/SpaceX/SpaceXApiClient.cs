using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Infrastructure.ExternalApis.SpaceX.Constants;
using SpaceX.Infrastructure.ExternalApis.SpaceX.Mappings;
using SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

using ContractRexponses = SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX;

public class SpaceXApiClient : ISpaceXApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SpaceXApiClient> _logger;

    public SpaceXApiClient(
        HttpClient httpClient,
        ILogger<SpaceXApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<LaunchResponse?> GetLatestLaunchAsync(CancellationToken cancellationToken)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV5}/{WebApiRouteConstants.Launches}/{WebApiRouteConstants.Latest}";

        try
        {
            _logger.LogInformation("Sending request to get latest launch from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError("SpaceX API request failed. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractRexponses.LaunchResponse>(cancellationToken);

            _logger.LogInformation("Successfully received latest launch from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API.");

            throw;
        }
    }

    public async Task<PaginatedLaunchesResponse?> GetLaunchesAsync(GetLaunchesRequest request, CancellationToken cancellationToken)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV5}/{WebApiRouteConstants.Launches}/{WebApiRouteConstants.Query}";

        try
        {
            _logger.LogInformation(
                "Sending request to get launches from SpaceX API. Uri: {Uri}",
                uri);

            var content = JsonContent.Create(request.ToContract());

            using var response = await _httpClient.PostAsync(uri, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get launches from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractRexponses.PaginatedLaunchesResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received launches from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for launches.");

            throw;
        }
    }
}
