using System.Net.Http.Json;
using FixLife.Admin.Users.Exceptions;
using FixLife.Admin.Users.Models;
using Microsoft.Extensions.Configuration;

namespace FixLife.Admin.Users.Extensions
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientApiUri;

        public HttpClientService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _clientApiUri = config["ClientApiUri"] 
                ?? throw new ClientUriNotConfigureException();
        }

        public async Task<(short statusCode, string message)> ForceLogoutUserAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    $"{_clientApiUri}/api/Account/LogoutForce/{userId}", 
                    null);

                if (response.IsSuccessStatusCode)
                {
                    return (200, "User logged out successfully");
                }
                else
                {
                    return ((short)response.StatusCode, $"Failed to logout user: {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return (500, $"Communication error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (500, $"Unexpected error: {ex.Message}");
            }
        }

        public async Task<(short statusCode, string message)> ResetUserPasswordAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.PutAsync(
                    $"{_clientApiUri}/api/Account/ResetPassword/{userId}", 
                    null);

                if (response.IsSuccessStatusCode)
                {
                    return (200, "Password reset initiated successfully");
                }
                else
                {
                    return ((short)response.StatusCode, $"Failed to reset password: {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                return (500, $"Communication error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (500, $"Unexpected error: {ex.Message}");
            }
        }
    }
} 