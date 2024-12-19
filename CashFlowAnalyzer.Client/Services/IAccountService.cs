using CashFlowAnalyzer.Shared.Models;

namespace CashFlowAnalyzer.Client.Services;

public interface IAccountService
{
    Task<bool> Login(LoginModel model);
    Task<bool> Logout();
    Task<bool> Register(RegisterModel model);
}