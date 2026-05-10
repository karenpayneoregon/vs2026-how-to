using Microsoft.Data.SqlClient;

namespace ExtensionsLibrary;

internal static class SqlClientExtensions
{

    extension(SqlDataReader reader)
    {
        public DateOnly GetDateOnly(int index)
        => reader.GetFieldValue<DateOnly>(index);

        public async Task<DateOnly> GetDateOnlyAsync(int index)
            => await reader.GetFieldValueAsync<DateOnly>(index);

        public async Task<DateTime> GetDateTimeAsync(int index)
            => await reader.GetFieldValueAsync<DateTime>(index);

        public async Task<DateTimeOffset> GetDateTimeOffsetAsync(int index)
            => await reader.GetFieldValueAsync<DateTimeOffset>(index);

        public async Task<string> GetStringAsync(int index)
            => await reader.GetFieldValueAsync<string>(index);

        public async Task<int> GetIntAsync(int index)
            => await reader.GetFieldValueAsync<int>(index);

        public async Task<decimal> GetDecimalAsync(int index)
            => await reader.GetFieldValueAsync<decimal>(index);

        public async Task<double> GetDoubleAsync(int index)
            => await reader.GetFieldValueAsync<double>(index);

        public TimeOnly GetTimeOnly(int index)
            => reader.GetFieldValue<TimeOnly>(index);

        public async Task<TimeOnly> GetTimeOnlyAsync(int index)
            => await reader.GetFieldValueAsync<TimeOnly>(index);
    }

    extension(TimeSpan sender)
    {
        public TimeOnly ToTimeOnly()
        => TimeOnly.FromTimeSpan(sender);
    }
}