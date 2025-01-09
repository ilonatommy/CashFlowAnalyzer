using System.Linq;

namespace CashFlowAnalyzer.Client.FinancialData;

public static class Categories
{
    // ToDo: change from readonly when we add a feature of creating new categories per-user
    private static readonly List<Category> allCategories = new List<Category>
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

    public static IEnumerable<Category> GetAllCategories() => allCategories;

    public static IEnumerable<Category> GetNotIgnoredCategories() => allCategories.Where(c => c.Mode != SharingMode.Ignored);

    public static Category GetCategoryByName(string name)
    {
        var allCategories = GetAllCategories();
        return allCategories.FirstOrDefault(c => c.ToString() == name) ??
            allCategories.First(c => c.Type == CategoryType.RequiresReview);
    }
}