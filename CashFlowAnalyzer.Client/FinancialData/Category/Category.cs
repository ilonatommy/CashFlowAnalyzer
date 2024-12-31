using System.ComponentModel;
using System.Globalization;

namespace CashFlowAnalyzer.Client.FinancialData;


[TypeConverter(typeof(CategoryConverter))]
public class Category
{
    public CategoryType Type { get; set; }
    public SharingMode Mode { get; set; }

    public Category(CategoryType type, SharingMode mode)
    {
        Type = type;
        Mode = mode;
    }
    public override string ToString() => Type.ToFriendlyString();

    public bool RequiresReview() => Mode == SharingMode.RequiresReview;
}

// Blazor framework is trying to bind a Category object directly to a <select> element,
// but it doesn't know how to convert between the Category object and a string
public class CategoryConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string str)
        {
            var parts = str.Split(new[] { " (" }, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                var type = (CategoryType)Enum.Parse(typeof(CategoryType), parts[0]);
                var mode = (SharingMode)Enum.Parse(typeof(SharingMode), parts[1].TrimEnd(')'));
                return new Category(type, mode);
            }
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is Category category)
        {
            return category.ToString();
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
