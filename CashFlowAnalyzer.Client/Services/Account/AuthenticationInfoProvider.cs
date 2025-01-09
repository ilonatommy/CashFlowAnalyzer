using Microsoft.AspNetCore.Components.Authorization;

namespace CashFlowAnalyzer.Client.Services;

public class AuthenticationInfoProvider
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationInfoProvider(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> IsAuthenticated()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User.Identity.IsAuthenticated;
    }
}