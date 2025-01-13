# Cash Flow Analyzer

Use the Analyzer to automate the review of your bank operations across multiple accounts in one application. It's an alternative to open banking that you control. You manually upload the statements in .xlsx form for the supported banks that is processed directly on your client device. You assign each transaction a category and only that collective data is sent back to the server. Category assignment can be improved by using automated classification algorithms from `Microsoft.ML` later.

This application is created with

``` bash
dotnet new blazor --interactivity Auto --all-interactive
```

## Supported banks

### Česká spořitelna

You can find the `Export` button in the account view in the browser version of your internet banking.
Choose .xlsx for the dates that you're interested in (custom - rozmezí), select fields:

- processing date (datum zaúčtování)
- partner (protiúčet)
- currency (měna)
- category (kategorie)

### Raiffeisen Bank

In the home page of the browser version of your internet banking, click `Transactions` button on the account you would like to export operations from.
In the filter select:

- outgoing (odchozí)
- dates you're interested in (datum)

Apply the filters (hledat) and click .csv button to export the transactions. The document contains transfer notes that can contain column separators and unintentionally break cvs loading mechanism. To make sure the file is loaded correctly, convert it to `.xlsx` format ("Excel Worksheet"), e.g. using MS Excel `Save As` option.
