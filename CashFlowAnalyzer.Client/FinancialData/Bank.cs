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
}