namespace CashFlowAnalyzer.Client.FinancialData;

public sealed class RaiffeisenRecord : SpreadsheetRecordBase
{
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Merchant { get; set; } = string.Empty;
}