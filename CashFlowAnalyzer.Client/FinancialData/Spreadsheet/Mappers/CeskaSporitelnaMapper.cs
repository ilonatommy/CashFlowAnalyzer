namespace CashFlowAnalyzer.Client.FinancialData;

public class CeskaSporitelnaMapper : FinancialRecordMapper
{
    public CeskaSporitelnaMapper(CurrencyMapper currencyMapper, string payer) : base(currencyMapper, payer)
    { }

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

    public FinancialRecord Map(CeskaSporitelnaRecord spreadsheetRecord)
    {
        int partnerAccountNumber = StringToInt(spreadsheetRecord.PartnerAccountNumber);
        decimal outgoingAmount = StringToDecimal(spreadsheetRecord.OutgoingAmount);
        ICurrency transactionCurrency = currencyMapper.Map(spreadsheetRecord.Currency);
        decimal convertedValue = currencyMapper.GetConvertedValue(outgoingAmount, transactionCurrency);

        return new FinancialRecord()
        {
            ProcessingDate = spreadsheetRecord.ProcessingDate,
            Recipient = new Recipient(spreadsheetRecord.PartnerName, partnerAccountNumber),
            Value = outgoingAmount,
            TransactionCurrency = transactionCurrency,
            ConvertedValue = convertedValue,
            Category = CeskaSporitelnaCategoryMapper.Map(spreadsheetRecord.Category),
            Bank = Bank.CeskaSporitelna,
            Payer = payer
        };
    }
}