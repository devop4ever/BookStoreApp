using BookStore.UI.Srv.Services.Base;

namespace BookStore.UI.Srv.Services.Authentication;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(LoginUserDto loginModel);
}
