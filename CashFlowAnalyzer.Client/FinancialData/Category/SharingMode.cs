using System.ComponentModel;

namespace CashFlowAnalyzer.Client.FinancialData;

public enum SharingMode
{

    /// <summary>
    /// used exclusively by the payer
    /// </summary>
    [Description("Private")]
    Private,

    /// <summary>
    /// used exclusively by the other payer
    /// </summary>
    [Description("Private on behalf")]
    PrivateOnBehalf,

    /// <summary>
    /// 50/50
    /// </summary>
    [Description("Shared")]
    Shared,

    /// <summary>
    /// skip it when displaying, e.g. own transfers or incomes
    /// </summary>
    [Description("Ignored")]
    Ignored,

    /// <summary>
    /// e.g. paying on behalf of group of friends
    /// </summary>
    [Description("Requires review")]
    RequiresReview,
}

public static class SharingModeExtensions
{
    public static string ToFriendlyString(this SharingMode bank)
    {
        var type = bank.GetType();
        var memInfo = type.GetMember(bank.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : bank.ToString();
    }
}