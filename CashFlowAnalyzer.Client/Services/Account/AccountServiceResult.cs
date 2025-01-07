namespace CashFlowAnalyzer.Client.Services;

public class AccountServiceResult
{
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
}