using Blazored.LocalStorage;
using BookStore.UI.Srv.Services.Base;
using BookStore.UI.Srv.Services.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.UI.Srv.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IClient httpClient;
    private readonly ILocalStorageService localStorage;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        this.httpClient = httpClient;
        this.localStorage = localStorage;
        this.authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
    {
        var response = await httpClient.LoginAsync(loginModel);

        //store token
        await localStorage.SetItemAsync("accessToken", response.Token);

        //change auth state 
        await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();


        return true;
    }
}
