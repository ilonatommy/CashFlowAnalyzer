
using CashFlowAnalyzer.Shared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CashFlowAnalyzer.Client.Services;

public class AccountService : IAccountService
{
    // workaround for a bug? https://github.com/dotnet/aspnetcore/issues/51986
    // or misconfiguration https://github.com/dotnet/aspnetcore/issues/51468
    private string baseAddress = "http://localhost:5130";
    private readonly HttpClient _http;
    private readonly ILogger<AccountService> _log;
    public AccountService(HttpClient http, ILogger<AccountService> log)
    {
        _http = http;
        _log = log;
    }

    public async Task<AccountServiceResult> Login(LoginModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{baseAddress}/api/auth/login");
        request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        return await SendRequest(request);
    }

    public async Task<AccountServiceResult> Logout()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{baseAddress}/api/auth/logout");
        return await SendRequest(request);
    }

    public async Task<AccountServiceResult> Register(RegisterModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{baseAddress}/api/auth/register");
        request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        return await SendRequest(request);
    }

    public async Task<bool> IsAuthenticated()
    {
        var response = await _http.GetAsync($"{baseAddress}/api/auth/isAuthenticated");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        return false;
    }

    private async Task<AccountServiceResult> SendRequest(HttpRequestMessage request)
    {
        using var response = await _http.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            _log.LogInformation("Request was successful");
            return new AccountServiceResult() { Success = true };
        }
        _log.LogWarning("Request failed");
        var errors = await response.Content.ReadFromJsonAsync<List<string>>();
        return new AccountServiceResult() { Success = false, Errors = errors };
    }
}