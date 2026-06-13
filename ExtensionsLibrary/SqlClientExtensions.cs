using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace ExtensionsLibrary;

internal static class SqlClientExtensions
{

    extension(SqlDataReader reader)
    {
        [DebuggerStepThrough]
        public DateOnly GetDateOnly(int index)
        => reader.GetFieldValue<DateOnly>(index);

        [DebuggerStepThrough]
        public async Task<DateOnly> GetDateOnlyAsync(int index)
            => await reader.GetFieldValueAsync<DateOnly>(index);

        [DebuggerStepThrough]
        public async Task<DateTime> GetDateTimeAsync(int index)
            => await reader.GetFieldValueAsync<DateTime>(index);

        [DebuggerStepThrough]
        public async Task<DateTimeOffset> GetDateTimeOffsetAsync(int index)
            => await reader.GetFieldValueAsync<DateTimeOffset>(index);

        [DebuggerStepThrough]
        public async Task<string> GetStringAsync(int index)
            => await reader.GetFieldValueAsync<string>(index);

        [DebuggerStepThrough]
        public async Task<int> GetIntAsync(int index)
            => await reader.GetFieldValueAsync<int>(index);

        [DebuggerStepThrough]
        public async Task<decimal> GetDecimalAsync(int index)
            => await reader.GetFieldValueAsync<decimal>(index);

        [DebuggerStepThrough]
        public async Task<double> GetDoubleAsync(int index)
            => await reader.GetFieldValueAsync<double>(index);

        [DebuggerStepThrough]
        public TimeOnly GetTimeOnly(int index)
            => reader.GetFieldValue<TimeOnly>(index);

        [DebuggerStepThrough]
        public async Task<TimeOnly> GetTimeOnlyAsync(int index)
            => await reader.GetFieldValueAsync<TimeOnly>(index);
    }

    extension(TimeSpan sender)
    {
        [DebuggerStepThrough]
        public TimeOnly ToTimeOnly()
        => TimeOnly.FromTimeSpan(sender);
    }
}