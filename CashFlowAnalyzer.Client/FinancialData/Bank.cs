using System.ComponentModel;

namespace CashFlowAnalyzer.Client.FinancialData;

public enum Bank
{
    [Description("Raiffeisen Bank")]
    Raiffeisen,
    [Description("Česká Spořitelna")]
    CeskaSporitelna,
    [Description("Moneta Money Bank")]
    Moneta,
    [Description("Wise")]
    Wise
}

public static class BankExtensions
{
    public static string ToFriendlyString(this Bank bank)
    {
        var type = bank.GetType();
        var memInfo = type.GetMember(bank.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : bank.ToString();
    }

    public static Bank FromDescription(this string description)
    {
        var type = typeof(Bank);
        foreach (var field in type.GetFields())
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute != null && attribute.Description == description)
            {
                return (Bank)field.GetValue(null);
            }
        }
        throw new ArgumentException($"No enum value with description '{description}' found in {type.Name}");
    }
}