namespace CashFlowAnalyzer.Client.FinancialData;

public class CurrencyEuro : ICurrency
{
    public string Name { get => "Euro"; }

    // ToDo: this should be pulled from converter API
    private decimal rate = 1.0M;
    public decimal RateToEuro { get => rate; set => rate = value; }
}