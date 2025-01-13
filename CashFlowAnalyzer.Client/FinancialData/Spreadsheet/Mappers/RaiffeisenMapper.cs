namespace CashFlowAnalyzer.Client.FinancialData;

public class RaiffeisenMapper : FinancialRecordMapper
{
    public RaiffeisenMapper(CurrencyMapper currencyMapper, string payer) : base(currencyMapper, payer)
    { }

    public List<FinancialRecord> Map(List<RaiffeisenRecord> spreadsheetRecords)
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

    public FinancialRecord Map(RaiffeisenRecord spreadsheetRecord)
    {
        string partnerName = !string.IsNullOrEmpty(spreadsheetRecord.PartnerName)
            ? $"{spreadsheetRecord.PartnerName} {spreadsheetRecord.Note}"
            : $"{spreadsheetRecord.Merchant} {spreadsheetRecord.Note}";
        int partnerAccountNumber = StringToInt(spreadsheetRecord.PartnerAccountNumber);
        decimal outgoingAmount = StringToDecimal(spreadsheetRecord.OutgoingAmount);
        ICurrency transactionCurrency = currencyMapper.Map(spreadsheetRecord.Currency);
        decimal convertedValue = currencyMapper.GetConvertedValue(outgoingAmount, transactionCurrency);

        return new FinancialRecord()
        {
            ProcessingDate = spreadsheetRecord.ProcessingDate,
            Recipient = new Recipient(partnerName, partnerAccountNumber),
            Value = outgoingAmount,
            TransactionCurrency = transactionCurrency,
            ConvertedValue = convertedValue,
            Category = RaiffeisenCategoryMapper.Map(
                spreadsheetRecord.TransactionType,
                spreadsheetRecord.Merchant
            ),
            Bank = Bank.Raiffeisen,
            Payer = payer
        };
    }
}