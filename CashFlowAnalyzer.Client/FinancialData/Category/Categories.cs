namespace CashFlowAnalyzer.Client.FinancialData;

public static class Categories
{
    public static List<Category> GetAllCategories()
    {
        return new List<Category>
        {
            new Category(CategoryType.Fees, SharingMode.Shared),
            new Category(CategoryType.PhoneBills, SharingMode.Private),
            new Category(CategoryType.OwnTransfer, SharingMode.Ignored),
            new Category(CategoryType.Groceries, SharingMode.Shared),
            new Category(CategoryType.Income, SharingMode.Ignored),
            new Category(CategoryType.Restaurant, SharingMode.RequiresReview),
            new Category(CategoryType.Bar, SharingMode.RequiresReview),
            new Category(CategoryType.Services, SharingMode.RequiresReview),
            new Category(CategoryType.Sport, SharingMode.RequiresReview),
            new Category(CategoryType.Transport, SharingMode.RequiresReview),
            new Category(CategoryType.RequiresReview, SharingMode.RequiresReview),
            new Category(CategoryType.Gifts, SharingMode.RequiresReview),
            new Category(CategoryType.Home, SharingMode.Shared)
        };
    }
}