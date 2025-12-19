using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ServiceDesk.Web.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ServiceDesk.Web.Services.Auth;

public class AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider)
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<Result<LoginResponse>>();

            if (response.IsSuccessStatusCode && result != null && result.Succeeded)
            {
                await localStorage.SetItemAsync("authToken", result.Data!.Token);

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.Data.Token);

                ((CustomAuthStateProvider)authStateProvider).NotifyUserAuthentication(result.Data.Token);

                return result;
            }

            return result ?? new Result<LoginResponse> { Succeeded = false, Errors = ["Falha no login."] };
        }
        catch (Exception)
        {
            return new Result<LoginResponse> { Succeeded = false, Errors = ["Servidor indisponível."] };
        }
    }

    public async Task LogoutAsync()
    {
        await localStorage.RemoveItemAsync("authToken");
        httpClient.DefaultRequestHeaders.Authorization = null;
        ((CustomAuthStateProvider)authStateProvider).NotifyUserLogout();
    }
}
