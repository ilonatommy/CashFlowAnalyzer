using System.ComponentModel;

namespace CashFlowAnalyzer.Client.FinancialData;

public enum CategoryType
{
    /// <summary>
    /// electricity, internet, gas, SVJ
    /// </summary>
    [Description("Fees")]
    Fees,

    [Description("Phone bills")]
    PhoneBills,

    /// <summary>
    /// ignored in the summary, same as internal transfer
    /// </summary>
    [Description("Own transfer")]
    OwnTransfer,

    [Description("Groceries")]
    Groceries,

    /// <summary>
    /// ignored in the summary
    /// </summary>
    [Description("Income")]
    Income,

    /// <summary>
    /// eating out
    /// </summary>
    [Description("Restaurant")]
    Restaurant,

    /// <summary>
    /// going out, drinking out (not food)
    /// </summary>
    [Description("Bar")]
    Bar,

    /// <summary>
    /// bike/car repairs, hairdressers, dry cleaners, flat renovation
    /// </summary>
    [Description("Services")]
    Services,

    /// <summary>
    /// including multisport
    /// </summary>
    [Description("Sport")]
    Sport,

    /// <summary>
    /// flights, car rental, taxi, litacka
    /// </summary>
    [Description("Transport")]
    Transport,

    /// <summary>
    /// bDay presents etc
    /// </summary>
    [Description("Gifts")]
    Gifts,

    /// <summary>
    /// requires manual categorizing
    /// </summary>
    [Description("Requires review")]
    RequiresReview
}

public static class CategoryTypeExtensions
{
    public static string ToFriendlyString(this CategoryType bank)
    {
        var type = bank.GetType();
        var memInfo = type.GetMember(bank.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : bank.ToString();
    }
}