using System.Net.Http.Json;
using CashFlowAnalyzer.Client.FinancialData;
using CashFlowAnalyzer.Shared.Models;

namespace CashFlowAnalyzer.Client.Services;
public class FinancialDataService : IFinancialDataService
{
    // same workaround as in AccountService
    private string baseAddress = "http://localhost:5130";
    private readonly HttpClient _httpClient;

    public FinancialDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SaveFinancialRecordsAsync(IEnumerable<FinancialRecord> records)
    {
        List<FinancialRecordDto> recordDtos = new();
        foreach (var record in records)
        {
            var recordDto = new FinancialRecordDto()
            {
                ProcessingDate = record.ProcessingDate,
                Value = record.Value,
                TransactionCurrency = record.TransactionCurrency.ToString(),
                Category = record.Category.ToString(),
                Bank = record.Bank.ToFriendlyString()
            };
            recordDtos.Add(recordDto);
        }
        var response = await _httpClient.PostAsJsonAsync($"{baseAddress}/api/financialrecords", recordDtos);
        response.EnsureSuccessStatusCode();
    }
}