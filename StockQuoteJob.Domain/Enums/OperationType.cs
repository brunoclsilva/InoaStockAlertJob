using System.ComponentModel;
using System.Reflection;

namespace StockQuoteJob.Domain.Enums
{
    public enum OperationType
    {
        [Description("venda")]
        Sell = 1,
        [Description("compra")]
        Buy = 2
    }

    public static class EnumExtension
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}