using System.Net.Http.Json;

using Microsoft.Extensions.Logging;

using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Infrastructure.ExternalApis.SpaceX.Constants;
using SpaceX.Infrastructure.ExternalApis.SpaceX.Mappings;
using SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

using ContractResponses = SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

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

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.LaunchResponse>(cancellationToken);

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

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.PaginatedLaunchesResponse>(cancellationToken);

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

    public async Task<LaunchResponse?> GetLaunchDetailsAsync(string launchId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV5}/{WebApiRouteConstants.Launches}/{launchId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get rocket details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get rocket details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.LaunchResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received rocket details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for rocket details.");

            throw;
        }
    }

    public async Task<RocketResponse?> GetRocketDetailsAsync(string rocketId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Rockets}/{rocketId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get rocket details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get rocket details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.RocketResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received rocket details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for rocket details.");

            throw;
        }
    }

    public async Task<LaunchpadResponse?> GetLaunchpadDetailsAsync(string launchpadId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Launchpads}/{launchpadId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get launchpad details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get launchpad details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.LaunchpadResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received launchpad details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for launchpad details.");

            throw;
        }
    }

    public async Task<LandpadResponse?> GetLandpadDetailsAsync(string landpadId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Landpads}/{landpadId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get landpad details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get landpad details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.LandpadResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received landpad details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for landpad details.");

            throw;
        }
    }

    public async Task<CrewMemberResponse?> GetCrewMemberDetailsAsync(string crewMemberId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Crew}/{crewMemberId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get crew member details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get crew member details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.CrewMemberResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received crew member details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for crew member details.");

            throw;
        }
    }

    public async Task<CapsuleResponse?> GetCapsuleDetailsAsync(string capsuleId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Capsules}/{capsuleId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get capsule details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get capsule details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.CapsuleResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received capsule details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for capsule details.");

            throw;
        }
    }

    public async Task<ShipResponse?> GetShipDetailsAsync(string shipId, CancellationToken cancellationToken = default)
    {
        var uri = $"{WebApiRouteConstants.ApiVersionV4}/{WebApiRouteConstants.Ships}/{shipId}";

        try
        {
            _logger.LogInformation(
                "Sending request to get ship details from SpaceX API. Uri: {Uri}",
                uri);

            using var response = await _httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError(
                    "Failed to get ship details from SpaceX API. StatusCode: {StatusCode}. Response: {Response}",
                    response.StatusCode, errorContent);

                throw new HttpRequestException(
                    $"SpaceX API request failed with status code {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<ContractResponses.ShipResponse>(cancellationToken);

            _logger.LogInformation(
                "Successfully received ship details from SpaceX API. Uri: {Uri}",
                uri);

            return result?.ToDomain();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling SpaceX API for ship details.");

            throw;
        }
    }
}
