using CashFlowAnalyzer.Client.FinancialData;

public class CeskaSporitelnaCategoryMapper : ICategoryMapper
{
    // ToDo: save the map in db and mechanism to add own categories
    private readonly Dictionary<string, Category> map = new()
    {
        { "Fees", Categories.Fees },
        { "Phone bills", Categories.PhoneBills },
        { "Internal transfer", Categories.OwnTransfer },
        { "Groceries", Categories.Groceries },
        { "Other Income", Categories.Income },
        { "Own Transfer", Categories.OwnTransfer },
        { "Restaurant/Café", Categories.Restaurant },
        { "Capitalization products", Categories.Income },
        { "Maintenance/Service", Categories.Services },
        { "Online Shops", Categories.RequiresReview }
    };

    public Category Map(string category)
    {
        if (map.TryGetValue(category, out var mappedCategory))
        {
            return mappedCategory;
        }
        return Categories.RequiresReview;
    }
}