namespace CashFlowAnalyzer.Client.FinancialData;

public enum SharingMode
{

    /// <summary>
    /// used exclusively by the payer
    /// </summary>
    Private,

    /// <summary>
    /// used exclusively by the other payer
    /// </summary>
    PrivateOnBehalf,

    /// <summary>
    /// 50/50
    /// </summary>
    Shared,

    /// <summary>
    /// skip it when displaying, e.g. own transfers or incomes
    /// </summary>
    Ignored,

    /// <summary>
    /// e.g. paying on behalf of group of friends
    /// </summary>
    RequiresReview,
}