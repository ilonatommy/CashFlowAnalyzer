
using CashFlowAnalyzer.Shared.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CashFlowAnalyzer.Client.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _http;
    private readonly ILogger<AccountService> _log;
    public AccountService(HttpClient http, ILogger<AccountService> log)
    {
        _http = http;
        _log = log;
    }

    public async Task<bool> Login(LoginModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/login");
        request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        using var response = await _http.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _log.LogInformation("Login successful for user {Username}", model.Username);
            return true;
        }
        _log.LogWarning("Login failed for user {Username}", model.Username);
        return false;
    }

    public Task<bool> Logout()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterModel model)
    {
        throw new NotImplementedException();
    }
}