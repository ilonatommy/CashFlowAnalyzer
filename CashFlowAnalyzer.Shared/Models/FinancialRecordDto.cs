namespace CashFlowAnalyzer.Shared.Models;

public sealed class FinancialRecordDto
{
    // ToDo: maybe change strings into currency/category/bank classes
    // ToDo: this is supposed to change once we start supporting more banks
    public int Id { get; set; }
    public DateTime ProcessingDate { get; set; }
    public decimal Value { get; set; }
    public string TransactionCurrency { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public string Payer { get; set; } = string.Empty;
}