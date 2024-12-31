namespace CashFlowAnalyzer.Client.FinancialData;

public sealed class FinancialRecord
{
    public DateTime ProcessingDate { get; set; }
    public Recipient Recipient { get; set; }
    public decimal Value { get; set; }
    public ICurrency TransactionCurrency { get; set; }
    public decimal ConvertedValue { get; set; }
    public Category Category { get; set; }
    public Bank Bank { get; set; }
    public string Payer { get; set; } = string.Empty;

    public bool RequiresReview() => Category.Type == CategoryType.RequiresReview;
}