namespace CashFlowAnalyzer.Client.FinancialData;

public class CurrencyMapper
{
    // ToDo: on initialization - pull conversion rate data for currency members
    private static CurrencyCZK s_currencyCZK = new();
    private Dictionary<string, ICurrency> map = new Dictionary<string, ICurrency>() {
        { "CZK", s_currencyCZK },
        { "default", s_currencyCZK }
    };
    private ICurrency _targetCurrency;
    public CurrencyMapper(ICurrency targetCurrency)
    {
        _targetCurrency = targetCurrency;
    }

    public ICurrency Map(string code)
    {
        if (map.TryGetValue(code, out ICurrency value))
            return value;
        return map["default"];
    }

    public List<FinancialRecord> MapCurrency(List<FinancialRecord> financialRecords)
    {
        List<FinancialRecord> mappedFinancialRecords = new();
        foreach (var record in financialRecords)
        {
            var mappedRecord = new FinancialRecord()
            {
                ProcessingDate = record.ProcessingDate,
                Recipient = record.Recipient,
                Value = record.Value,
                TransactionCurrency = record.TransactionCurrency,
                ConvertedValue = GetConvertedValue(record.Value, record.TransactionCurrency),
                Category = record.Category,
                Bank = record.Bank,
                Payer = record.Payer

            };
            mappedFinancialRecords.Add(mappedRecord);
        }
        return mappedFinancialRecords;
    }

    public decimal GetConvertedValue(decimal value, ICurrency transactionCurrency)
    {
        return _targetCurrency.GetType() == transactionCurrency.GetType()
            ? value
            : value / transactionCurrency.RateToEuro * _targetCurrency.RateToEuro;
    }
}