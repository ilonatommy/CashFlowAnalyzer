using System.ComponentModel;
using System.Globalization;

namespace CashFlowAnalyzer.Client.FinancialData;

[TypeConverter(typeof(CurrencyConverter))]
public interface ICurrency
{
    public string Name { get; }
    public decimal RateToEuro { get; set; }
    public string ToString();
}

public class CurrencyConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string str)
        {
            // Implement your logic to convert from string to ICurrency
            // For example, you can use a factory method or a dictionary to get the correct ICurrency instance
            return Currencies.GetCurrencyByName(str);
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is ICurrency currency)
        {
            return currency.ToString();
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}