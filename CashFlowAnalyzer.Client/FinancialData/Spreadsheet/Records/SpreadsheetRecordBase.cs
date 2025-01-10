namespace CashFlowAnalyzer.Client.FinancialData;

public class SpreadsheetRecordBase
{
    public DateTime ProcessingDate { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public string PartnerAccountNumber { get; set; } = string.Empty;
    public string IncomingAmount { get; set; } = string.Empty;
    public string OutgoingAmount { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
}