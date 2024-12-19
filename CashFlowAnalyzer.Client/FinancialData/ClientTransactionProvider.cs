using System.Net.Http.Json;

namespace CashFlowAnalyzer.Client.FinancialData;

internal sealed class ClientTransactionProvider(HttpClient httpClient) : ITransactionProvider
{
    public async Task<IEnumerable<FinancialRecord>> GetTransactionsAsync() =>
        await httpClient.GetFromJsonAsync<FinancialRecord[]>("/weather-forecast") ??
            throw new IOException("No weather forecast!");
}