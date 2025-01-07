using CashFlowAnalyzer.Client.FinancialData;

namespace CashFlowAnalyzer.Client.Services;

public interface IFinancialDataService
{
    Task SaveFinancialRecordsAsync(IEnumerable<FinancialRecord> records);
}