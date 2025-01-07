using CashFlowAnalyzer.Server.Data;
using CashFlowAnalyzer.Shared.Models;

namespace CashFlowAnalyzer.Server.Services;

public class DatabaseService
{
    private readonly ApplicationDbContext _context;

    public DatabaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveFinancialRecordsAsync(IEnumerable<FinancialRecordDto> records)
    {
        _context.FinancialRecords.AddRange(records);
        await _context.SaveChangesAsync();
    }
}