using CashFlowAnalyzer.Shared.Models;
using CashFlowAnalyzer.Server.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetFinancialRecords()
    {
        var records = await _dbService.GetFinancialRecordsAsync();
        return Ok(records);
    }
}