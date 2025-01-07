namespace CashFlowAnalyzer.Client.FinancialData;

public class FinancialRecordMapper
{
    private CurrencyMapper currencyMapper = new();
    private CeskaSporitelnaCategoryMapper categoryMapper = new();
    private ICurrency targetCurrency;
    private string payer;
    public FinancialRecordMapper(ICurrency targetCurrency, string payer)
    {
        this.targetCurrency = targetCurrency;
        this.payer = payer;
    }

    public FinancialRecord Map(CeskaSporitelnaRecord spreadsheetRecord)
    {
        int partnerAccountNumber = StringToInt(spreadsheetRecord.PartnerAccountNumber);
        decimal outgoingAmount = StringToDecimal(spreadsheetRecord.OutgoingAmount);
        ICurrency transactionCurrency = currencyMapper.Map(spreadsheetRecord.Currency);
        decimal convertedValue = GetConvertedValue(outgoingAmount, transactionCurrency);

        return new FinancialRecord()
        {
            ProcessingDate = spreadsheetRecord.ProcessingDate,
            Recipient = new Recipient(spreadsheetRecord.PartnerName, partnerAccountNumber),
            Value = outgoingAmount,
            TransactionCurrency = transactionCurrency,
            ConvertedValue = convertedValue,
            Category = categoryMapper.Map(spreadsheetRecord.Category),
            Bank = Bank.CeskaSporitelna,
            Payer = payer
        };
    }
    public List<FinancialRecord> Map(List<CeskaSporitelnaRecord> spreadsheetRecords)
    {
        List<FinancialRecord> financialRecords = new();
        foreach(var record in spreadsheetRecords)
        {
            var financialRecord = Map(record);
            // 0-valued fees and incomes are not interesting
            if (financialRecord.Value != 0)
            {
                financialRecords.Add(financialRecord);
            }
        }
        return financialRecords;
    }

    private int StringToInt(string str)
    {
        if (!int.TryParse(str, out int ret))
        {
            return 0;
        }
        return ret;
    }

    private decimal StringToDecimal(string str)
    {
        if (!decimal.TryParse(str, out decimal ret))
        {
            return 0.0m;
        }
        return ret;
    }

    private decimal GetConvertedValue(decimal value, ICurrency transactionCurrency)
    {
        return targetCurrency.GetType() == transactionCurrency.GetType()
            ? value
            : value / transactionCurrency.RateToEuro * targetCurrency.RateToEuro;
    }
}