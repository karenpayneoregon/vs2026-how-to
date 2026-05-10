using Microsoft.Data.SqlClient;

namespace ExtensionsLibrary;
public static class SqlClientExtensions
{
    extension(SqlDataReader reader)
    {
        public DateOnly GetDateOnly(int index)
            => reader.GetFieldValue<DateOnly>(index);

        public TimeOnly GetTimeOnly(int index)
            => reader.GetFieldValue<TimeOnly>(index);

        public string GetTimeOnlyFormatted(int index)
            => reader.GetFieldValue<TimeOnly>(index).ToString("hh:mm tt");

        public string GetDateOnlyFormatted(int index)
            => reader.GetFieldValue<DateOnly>(index).ToString("MM/dd/yyyy");
    }
}
