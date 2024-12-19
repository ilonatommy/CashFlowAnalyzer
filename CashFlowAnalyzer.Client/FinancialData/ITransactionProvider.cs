namespace CashFlowAnalyzer.Client.FinancialData;

public interface ITransactionProvider
{
    Task<IEnumerable<FinancialRecord>> GetTransactionsAsync();
}