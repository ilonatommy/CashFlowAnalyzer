namespace CashFlowAnalyzer.Client.FinancialData;

public class CurrencyPLN : ICurrency
{
    public string Name { get => "PLN"; }

    // ToDo: this should be pulled from converter API
    private decimal rate = 4.27M;
    public decimal RateToEuro { get => rate; set => rate = value; }
    public override string ToString() => Name;
}