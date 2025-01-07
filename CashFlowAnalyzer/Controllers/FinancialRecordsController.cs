using CashFlowAnalyzer.Shared.Models;
using CashFlowAnalyzer.Server.Services;
using Microsoft.AspNetCore.Mvc;
using CashFlowAnalyzer.Client.FinancialData;

[ApiController]
[Route("api/[controller]")]
public class FinancialRecordsController : ControllerBase
{
    private readonly DatabaseService _dbService;

    public FinancialRecordsController(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveFinancialRecords([FromBody] IEnumerable<FinancialRecordDto> records)
    {
        if (records == null || !records.Any())
        {
            return BadRequest("No records to save.");
        }

        await _dbService.SaveFinancialRecordsAsync(records);
        return Ok();
    }
}