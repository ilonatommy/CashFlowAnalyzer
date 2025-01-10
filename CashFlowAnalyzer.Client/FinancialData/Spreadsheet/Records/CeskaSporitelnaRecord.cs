namespace CashFlowAnalyzer.Client.FinancialData;

public sealed class CeskaSporitelnaRecord : SpreadsheetRecordBase
{
    public string PartnerIBAN { get; set; } = string.Empty;
    public string BICSWIFT { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}