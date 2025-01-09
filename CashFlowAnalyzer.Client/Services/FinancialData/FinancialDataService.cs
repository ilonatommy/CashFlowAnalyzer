using System.Net.Http.Json;
using System.Text.Json;
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

    // ToDo: add filtering by dates/categories etc
    public async Task<IEnumerable<FinancialRecord>> GetFinancialRecordsAsync()
    {
        var response = await _httpClient.GetAsync($"{baseAddress}/api/financialrecords");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var recordDtos = JsonSerializer.Deserialize<List<FinancialRecordDto>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        List<FinancialRecord> records = new();
        foreach(var dto in recordDtos)
        {
            records.Add(new FinancialRecord()
            {
                ProcessingDate = dto.ProcessingDate,
                Value = dto.Value,
                TransactionCurrency = Currencies.GetCurrencyByName(dto.TransactionCurrency),
                Category = Categories.GetCategoryByName(dto.Category),
                Bank = dto.Bank.FromDescription(),
                Payer = dto.Payer
            });
        }

        return records;
    }
}