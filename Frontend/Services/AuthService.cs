using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Core.Dtos.Account;
using Frontend.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Services
{
    public class AuthService(
        HttpClient httpclient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider
    ) : IAuthService
    {

        public async Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var response = await httpclient.PostAsJsonAsync("api/account/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResultDto { IsAuthSuccessful = false, ErrorMessage = "Invalid email or password." };
            }

            var content = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResultDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (loginResult == null || string.IsNullOrEmpty(loginResult.Token))
            {
                return new LoginResultDto { IsAuthSuccessful = false, ErrorMessage = "Failed to process login response." };
            }

            await localStorage.SetItemAsync("authToken", loginResult.Token);

            ((CustomAuthStateProvider)authenticationStateProvider).NotifyUserAuthentication(loginResult.Token);

            return new LoginResultDto { IsAuthSuccessful = true };
        }

        public async Task LogoutAsync()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((CustomAuthStateProvider)authenticationStateProvider).NotifyUserLogout();
        }
    }
}
