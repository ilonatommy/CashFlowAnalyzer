namespace CashFlowAnalyzer.Client.FinancialData;

public class FinancialRecordMapper
{
    protected CurrencyMapper currencyMapper;
    protected string payer;
    public FinancialRecordMapper(CurrencyMapper currencyMapper, string payer)
    {
        this.currencyMapper = currencyMapper;
        this.payer = payer;
    }

    protected int StringToInt(string str)
    {
        if (!int.TryParse(str, out int ret))
        {
            return 0;
        }
        return ret;
    }

    protected decimal StringToDecimal(string str)
    {
        if (!decimal.TryParse(str, out decimal ret))
        {
            return 0.0m;
        }
        return ret;
    }

}