using System.Globalization;
using CashFlowAnalyzer.Client.FinancialData;
using OfficeOpenXml;

public class SpreadsheetReader
{
    public SpreadsheetReader()
    {
        // Set the license context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<List<CeskaSporitelnaRecord>> ReadCeskaSporitelnaSpreadsheetAsync(Stream stream)
    {
        var result = new List<CeskaSporitelnaRecord>();

        // Load the spreadsheet asynchronously
        using (var memoryStream = new MemoryStream())
        {
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Assuming the data is in the first worksheet

                // Read the column titles from the 3rd row
                var columnTitles = new List<string>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    columnTitles.Add(worksheet.Cells[3, col].Text);
                }

                // Read the data starting from the 4th row
                for (int row = 4; row <= worksheet.Dimension.End.Row; row++)
                {
                    var record = new CeskaSporitelnaRecord();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Text;
                        switch (columnTitles[col - 1])
                        {
                            case "Processing Date":
                                if (string.IsNullOrEmpty(cellValue))
                                {
                                    col = worksheet.Dimension.End.Row;
                                    break;
                                }
                                record.ProcessingDate = ParseDate(cellValue);
                                break;
                            case "Partner Name":
                                record.PartnerName = cellValue;
                                break;
                            case "Partner IBAN":
                                record.PartnerIBAN = cellValue;
                                break;
                            case "BIC/SWIFT":
                                record.BICSWIFT = cellValue;
                                break;
                            case "Partner Account Number":
                                record.PartnerAccountNumber = cellValue;
                                break;
                            case "Bank Code":
                                record.BankCode = cellValue;
                                break;
                            case "Incoming Amount":
                                record.IncomingAmount = cellValue;
                                break;
                            case "Outgoing Amount":
                                record.OutgoingAmount = cellValue;
                                break;
                            case "Currency":
                                record.Currency = cellValue;
                                break;
                            case "Category":
                                record.Category = cellValue;
                                break;
                        }
                    }
                    result.Add(record);
                }
            }
        }
        return result;
    }

    private string[] dateFormats = ["dd.MM.yyyy", "MM/dd/yyyy"];

    private DateTime ParseDate(string cellValue)
    {
        if (DateTime.TryParse(cellValue, out var processingDate) ||
            DateTime.TryParseExact(cellValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out processingDate))
        {
            return processingDate;
        }
        throw new FormatException($"Invalid date format: {cellValue}");
    }
}