namespace CashFlowAnalyzer.Client.FinancialData;

public class Category
{
    public string DisplayName { get; set; } = string.Empty;
    public SharingMode Mode { get; set; }

    public Category(string displayName, SharingMode mode)
    {
        DisplayName = displayName;
        Mode = mode;
    }
}

public static class Categories
{
    /// <summary>
    /// electricity, internet, gas, SVJ
    /// </summary>
    public static Category Fees => new("Fees", SharingMode.Shared);
    public static Category PhoneBills => new("Phone bills", SharingMode.Private);
    public static Category Groceries => new("Groceries", SharingMode.Shared);

    /// <summary>
    /// ignored in the summary, same as internal transfer
    /// </summary>
    public static Category OwnTransfer => new("Own transfer", SharingMode.Ignored);

    /// <summary>
    /// ignored in the summary
    /// </summary>
    public static Category Income => new("Income", SharingMode.Ignored);

    /// <summary>
    /// eating out
    /// </summary>
    public static Category Restaurant => new("Restaurant", SharingMode.RequiresReview);

    /// <summary>
    /// going out, drinking out (not food)
    /// </summary>
    public static Category Bar => new("Bar", SharingMode.RequiresReview);

    /// <summary>
    /// including multisport
    /// </summary>
    public static Category Sports => new("Sports", SharingMode.RequiresReview);

    /// <summary>
    /// flights, car rental, taxi, litacka
    /// </summary>
    public static Category Transport => new("Transport", SharingMode.RequiresReview);

    /// <summary>
    /// bike/car repairs, hairdressers, dry cleaners, flat renovation
    /// </summary>
    public static Category Services => new("Services", SharingMode.RequiresReview);

    /// <summary>
    /// bDay presents etc
    /// </summary>
    public static Category Gifts => new("Gift", SharingMode.RequiresReview);

    /// <summary>
    /// requires manual categorizing
    /// </summary>
    public static Category RequiresReview => new("unknown", SharingMode.RequiresReview);
}