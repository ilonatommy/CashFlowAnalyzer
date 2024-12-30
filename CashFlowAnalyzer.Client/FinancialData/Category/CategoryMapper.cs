using CashFlowAnalyzer.Client.FinancialData;

public class CeskaSporitelnaCategoryMapper : ICategoryMapper
{
    // ToDo: save the map in db and mechanism to add own CategoryType
    private readonly Dictionary<string, CategoryType> map = new()
    {
        { "Fees", CategoryType.Fees },
        { "Phone bills", CategoryType.PhoneBills },
        { "Internal transfer", CategoryType.OwnTransfer },
        { "Groceries", CategoryType.Groceries },
        { "Other Income", CategoryType.Income },
        { "Own Transfer", CategoryType.OwnTransfer },
        { "Restaurant/Café", CategoryType.Restaurant },
        { "Capitalization products", CategoryType.Income },
        { "Maintenance/Service", CategoryType.Services },
        { "Online Shops", CategoryType.RequiresReview }
    };

    public Category Map(string category)
    {
        CategoryType categoryType;
        if (!map.TryGetValue(category, out categoryType))
        {
            categoryType = CategoryType.RequiresReview;
        }
        return Categories.GetAllCategories().First(c => c.Type == categoryType);
    }
}