namespace CashFlowAnalyzer.Client.FinancialData;

public class CurrencyCZK : ICurrency
{
    public string Name { get => "CZK"; }

    // ToDo: this should be pulled from converter API
    private decimal rate = 25.21M;
    public decimal RateToEuro { get => rate; set => rate = value; }
    public override string ToString() => Name;
}