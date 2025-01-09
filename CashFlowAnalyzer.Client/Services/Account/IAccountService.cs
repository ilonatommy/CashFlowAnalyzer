using CashFlowAnalyzer.Shared.Models;

namespace CashFlowAnalyzer.Client.Services;

public interface IAccountService
{
    Task<AccountServiceResult> Login(LoginModel model);
    Task<AccountServiceResult> Logout();
    Task<AccountServiceResult> Register(RegisterModel model);
    Task<bool> IsAuthenticated();
}