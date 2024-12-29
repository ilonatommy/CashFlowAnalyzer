namespace CashFlowAnalyzer.Client.FinancialData;

public class CurrencyMapper
{
    // ToDo: on initialization - pull conversion rate data for currency members
    private static CurrencyCZK s_currencyCZK = new();
    private Dictionary<string, ICurrency> map = new Dictionary<string, ICurrency>() {
        { "CZK", s_currencyCZK },
        { "default", s_currencyCZK }
    };

    public ICurrency Map(string code)
    {
        if (map.TryGetValue(code, out ICurrency value))
            return value;
        return map["default"];
    }
}