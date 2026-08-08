using System.Globalization;

namespace ConversionsApp.Classes;

public static class StringExtensions
{
    extension(string? value)
    {
        public int ToInt(int defaultValue = 0) =>
            int.TryParse(value, out int result)
                ? result
                : defaultValue;

        public decimal ToDecimal(decimal defaultValue = 0m) =>
            decimal.TryParse(value, out decimal result)
                ? result
                : defaultValue;

        public int? ToNullableInt() =>
            int.TryParse(value, out int result)
                ? result
                : null;

        public decimal? ToNullableDecimal() =>
            decimal.TryParse(value, out decimal result)
                ? result
                : null;
    }
    public static DateOnly ToDateOnly(this string value)
    {
        return DateOnly.Parse(value, CultureInfo.InvariantCulture);
    }
}