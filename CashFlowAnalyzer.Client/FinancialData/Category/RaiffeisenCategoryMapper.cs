using CashFlowAnalyzer.Client.FinancialData;

public class RaiffeisenCategoryMapper
{
    private static readonly Dictionary<string, CategoryType> transactionTypesMap = new()
    {
        { "Single Payment", CategoryType.RequiresReview },
        { "Incoming Payment", CategoryType.Income },
        { "Incoming Instant Payment", CategoryType.Income },
        { "Outgoing Instant Payment", CategoryType.RequiresReview },
        { "StandingOrder", CategoryType.RequiresReview },
        { "Card Payment", CategoryType.RequiresReview },
        { "Bonus transfer to client account", CategoryType.Income }
    };

    // ToDo: this should be in a db and be configurable in an admin panel
    private static readonly Dictionary<string, CategoryType> merchantMap = new()
    {
        { "OBI", CategoryType.Home },
        { "BILLA", CategoryType.Groceries },
        { "T-Mobile", CategoryType.Fees },
        { "Ryanair", CategoryType.Transport },
        { "Koberce Breno", CategoryType.Home },
        { "Tesco", CategoryType.Groceries },
        { "Mercuria Laser Game", CategoryType.Sport },
        { "Dopravní podnik hlavního města Prahy - DPP", CategoryType.Transport },
        { "Technická správa komunikací hlavního města Prahy", CategoryType.Transport },
        { "Lezecké centrum SmíchOFF", CategoryType.Sport }
    };

    public static Category Map(string transactionType, string merchant)
    {
        CategoryType categoryType;
        if (transactionTypesMap.TryGetValue(transactionType, out categoryType)
        || merchantMap.TryGetValue(merchant, out categoryType))
        {
            if (categoryType != CategoryType.RequiresReview)
                return Categories.GetCategoryByName(categoryType.ToFriendlyString());
        }
        return Categories.GetAllCategories().First(c => c.Type == CategoryType.RequiresReview);
    }
}