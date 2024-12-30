namespace CashFlowAnalyzer.Client.FinancialData;

public static class Currencies
{
    public static List<ICurrency> GetAllCurrencies()
    {
        return new List<ICurrency>
        {
            new CurrencyCZK(),
            new CurrencyEuro(),
            new CurrencyPLN()
        };
    }

    // ToDo: this should be saved in the db per user
    public static ICurrency GetDefaultCurrency() => new CurrencyCZK();

    public static ICurrency GetCurrencyByName(string name) =>
        GetAllCurrencies().FirstOrDefault(c => c.Name == name) ?? GetDefaultCurrency();
}