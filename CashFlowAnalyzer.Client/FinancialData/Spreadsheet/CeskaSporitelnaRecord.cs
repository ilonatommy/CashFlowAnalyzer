namespace CashFlowAnalyzer.Client.FinancialData;

public sealed class CeskaSporitelnaRecord
{
    public DateTime ProcessingDate { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public string PartnerIBAN { get; set; } = string.Empty;
    public string BICSWIFT { get; set; } = string.Empty;
    public string PartnerAccountNumber { get; set; } = string.Empty;
    public string BankCode { get; set; } = string.Empty;
    public string IncomingAmount { get; set; } = string.Empty;
    public string OutgoingAmount { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}