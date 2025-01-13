using System.Globalization;
using CashFlowAnalyzer.Client.FinancialData;
using OfficeOpenXml;

namespace CashFlowAnalyzer.Client.Services;

public class SpreadsheetReader
{
    private string[] CeskaSporitelnaDateFormats = ["dd.MM.yyyy"];
    private string[] CeskaSporitelnaColumns = [
        "Processing Date",
        "Partner Name",
        "Partner IBAN",
        "BIC/SWIFT",
        "Partner Account Number",
        "Bank code",
        "Incoming Amount",
        "Outgoing Amount",
        "Currency",
        "Category"
    ];

    private string[] RaiffeisenDateFormats = ["MM.dd.yy H:mm"];
    private string[] RaiffeisenColumns = [
        "Transaction Date", // ignored
        "Booking Date",
        "Account number", // ignored
        "Account Name", // ignored
        "Transaction Category", // ignored
        "Accocunt Number", // typo originally
        "Name of Account",
        "Transaction type",
        "Message",
        "Note",
        "VS", // ignored
        "KS", // ignored
        "SS", // ignored
        "Booked amount",
        "Account Currency",
        "Original Amount and Currency", // ignored
        "Original Amount and Currency", // duplicated originally
        "Fee", // ignored
        "Transaction ID", // ignored
        "Note", // ignored
        "Merchant",
        "City" // ignored
    ];

    private string incorrectFormatError = "Loaded spreadsheet is not in a format expected for";

    public SpreadsheetReader()
    {
        // Set the license context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    // ToDo: we could support csv if the user removed unintentional column separators - on their responsibility
    private static async Task<(List<string[]>, List<string>)> LoadCsvAsync(Stream stream)
    {
        var rows = new List<string[]>();
        var columnTitles = new List<string>();

        using (var reader = new StreamReader(stream))
        {
            string line;
            bool isFirstRow = true;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var values = line.Split(';');

                if (isFirstRow)
                {
                    columnTitles.AddRange(values);
                    isFirstRow = false;
                }
                else
                {
                    rows.Add(values);
                }
            }
        }

        return (rows, columnTitles);
    }

    private static async Task<(ExcelPackage, ExcelWorksheet, List<string>)> LoadWorksheetAsync(Stream stream, int columnTitlesOffset)
    {
        using (var memoryStream = new MemoryStream())
        {
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var package = new ExcelPackage(memoryStream);

            var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet

            // Read the column titles from the 3rd row
            var columnTitles = new List<string>();
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                columnTitles.Add(worksheet.Cells[columnTitlesOffset, col].Text);
            }

            return (package, worksheet, columnTitles);
        }
    }

    public async Task<List<CeskaSporitelnaRecord>> ReadCeskaSporitelnaSpreadsheetAsync(Stream stream)
    {
        var result = new List<CeskaSporitelnaRecord>();
        var (package, worksheet, columnTitles) = await LoadWorksheetAsync(stream, 3);
        if (!VerifySpreadsheetColumns(columnTitles, CeskaSporitelnaColumns)!)
        {
            throw new Exception($"{incorrectFormatError} {Bank.CeskaSporitelna.ToFriendlyString()}.");
        }
        try
        {
            for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
            {
                var record = new CeskaSporitelnaRecord();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var cellValue = worksheet.Cells[row, col].Text;
                    var columnTitle = columnTitles[col - 1];
                    if (columnTitle == CeskaSporitelnaColumns[0])
                    {
                        if (string.IsNullOrEmpty(cellValue))
                        {
                            row = worksheet.Dimension.End.Row;
                            break;
                        }
                        record.ProcessingDate = CeskaSporitelnaParseDate(cellValue);
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[1])
                    {
                        record.PartnerName = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[2])
                    {
                        record.PartnerIBAN = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[3])
                    {
                        record.BICSWIFT = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[4])
                    {
                        record.PartnerAccountNumber = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[5])
                    {
                        record.BankCode = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[6])
                    {
                        record.IncomingAmount = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[7])
                    {
                        record.OutgoingAmount = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[8])
                    {
                        record.Currency = cellValue;
                    }
                    else if (columnTitle == CeskaSporitelnaColumns[9])
                    {
                        record.Category = cellValue;
                    }
                }
                result.Add(record);
            }
        }
        finally
        {
            package.Dispose();
        }
        
        return result;
    }

    public async Task<List<RaiffeisenRecord>> ReadRaiffeisenSpreadsheetAsync(Stream stream)
    {
        var result = new List<RaiffeisenRecord>();
        var (package, worksheet, columnTitles) = await LoadWorksheetAsync(stream, 1);
        if (!VerifySpreadsheetColumns(columnTitles, RaiffeisenColumns)!)
        {
            throw new Exception($"{incorrectFormatError} {Bank.Raiffeisen.ToFriendlyString()}.");
        }
        
        try
        {
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var record = new RaiffeisenRecord();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    var cellValue = worksheet.Cells[row, col].Text;
                    var columnTitle = columnTitles[col - 1];
                    if (columnTitle == RaiffeisenColumns[1])
                    {
                        if (string.IsNullOrEmpty(cellValue))
                        {
                            row = worksheet.Dimension.End.Row;
                            break;
                        }
                        record.ProcessingDate = RaiffeisenParseDate(cellValue);
                    }
                    if (string.IsNullOrEmpty(cellValue))
                        continue;
                    if (columnTitle == RaiffeisenColumns[5])
                    {
                        record.PartnerAccountNumber = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[6])
                    {
                        record.PartnerName = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[7])
                    {
                        record.TransactionType = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[8])
                    {
                        record.Message = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[9])
                    {
                        record.Note = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[13])
                    {
                        if (cellValue.StartsWith('-'))
                        {
                            record.OutgoingAmount = cellValue;
                        }
                        else
                        {
                            record.IncomingAmount = cellValue;
                        }
                    }
                    else if (columnTitle == RaiffeisenColumns[14])
                    {
                        record.Currency = cellValue;
                    }
                    else if (columnTitle == RaiffeisenColumns[20])
                    {
                        record.Merchant = cellValue;
                    }
                }
                result.Add(record);
            }
        }
        finally
        {
            package.Dispose();
        }

        return result;
    }

    private bool VerifySpreadsheetColumns(List<string> columnTitles, string[] expectedColumnTitles)
    {
        for (int i = 0; i < expectedColumnTitles.Length; i++)
        {
            if (columnTitles[i] != expectedColumnTitles[i])
                return false;
        }
        return true;
    }

    private DateTime CeskaSporitelnaParseDate(string cellValue) =>
        ParseDate(cellValue, CeskaSporitelnaDateFormats);

    private DateTime RaiffeisenParseDate(string cellValue) =>
        ParseDate(cellValue, RaiffeisenDateFormats);

    private DateTime ParseDate(string cellValue, string[] dateFormats)
    {
        if (DateTime.TryParse(cellValue, out var processingDate) ||
            DateTime.TryParseExact(cellValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out processingDate))
        {
            return processingDate;
        }
        throw new FormatException($"Invalid date format: {cellValue}");
    }
}