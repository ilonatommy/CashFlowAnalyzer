namespace CashFlowAnalyzer.Client.FinancialData;

public interface ICurrency
{
    public string Name { get; }
    public decimal RateToEuro { get; set; }
}