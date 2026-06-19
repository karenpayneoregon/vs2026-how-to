using System;
using System.Collections.Generic;
using System.Globalization;
using CommonLibrary.Models;
using static System.Globalization.DateTimeFormatInfo;

namespace CommonLibrary;

/// <summary>
/// Provides a collection of helper methods for working with dates, including operations
/// such as retrieving dates for the next week, dividing a month into weeks, and more.
/// </summary>
public static class DateOnlyHelpers
{

    /// <summary>
    /// Retrieves the dates for the prior week starting from the previous occurrence of Sunday.
    /// </summary>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the prior week.</returns>
    public static List<DateOnly> PriorWeeksDates()
        => GetPriorWeeksDates(DateOnly.FromDateTime(DateTime.Now));

    public static List<DateOnly> GetPriorWeeksDates(DateOnly dateTime)
    {
        var currentWeekSunday = dateTime.AddDays(-(int)dateTime.DayOfWeek);
        var priorWeekSunday = currentWeekSunday.AddDays(-7);

        return Enumerable.Range(0, 7)
            .Select(priorWeekSunday.AddDays)
            .ToList();
    }

    /// <summary>
    /// Retrieves the dates for the next week starting from the next occurrence of Sunday.
    /// </summary>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the next week.</returns>
    public static List<DateOnly> NextWeeksDates()
        => Enumerable.Range(0, 7).Select(index =>
                DateOnly.FromDateTime(DateTime.Now).Next(DayOfWeek.Sunday).AddDays(index))
            .ToList();

    /// <summary>
    /// Retrieves the dates for the current week, starting from the specified day of the week.
    /// </summary>
    /// <param name="weekStartsOn">
    /// The day of the week that marks the start of the week. Defaults to <see cref="DayOfWeek.Monday"/>.
    /// </param>
    /// <returns>
    /// A list of <see cref="DateOnly"/> objects representing the dates of the current week.
    /// </returns>
    public static List<DateOnly> GetCurrentWeekDates(DayOfWeek weekStartsOn = DayOfWeek.Monday)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        int daysSinceStartOfWeek =
            (7 + (today.DayOfWeek - weekStartsOn)) % 7;

        DateOnly startOfWeek = today.AddDays(-daysSinceStartOfWeek);

        return Enumerable.Range(0, 7)
            .Select(startOfWeek.AddDays)
            .ToList();
    }

    /// <summary>
    /// Retrieves the dates for the next week starting from the next occurrence of Sunday
    /// based on the provided starting date.
    /// </summary>
    /// <param name="dateTime">The starting date to calculate from.</param>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the next week.</returns>
    public static List<DateOnly> GetNextWeeksDates(DateOnly dateTime)
        => Enumerable.Range(0, 7).Select(index =>
                dateTime.Next(DayOfWeek.Sunday).AddDays(index))
            .ToList();

    
    /// <summary>
    /// Retrieves all the days of the specified month in the current year.
    /// </summary>
    /// <param name="month">The month for which to retrieve the days (1 to 12).</param>
    /// <returns>A list of <see cref="DateOnly"/> objects representing each day in the specified month.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="month"/> is not in the range 1 to 12.
    /// </exception>
    public static List<DateOnly> GetMonthDays(int month)
    {
        var year = DateTime.Now.Year;

        return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
            .Select(day => new DateOnly(year, month, day))
            .ToList();
    }


    /// <summary>
    /// Divides the days of a specified month and year into weeks, where each week is represented as a list of days.
    /// </summary>
    /// <param name="year">The year of the month to process.</param>
    /// <param name="month">The month to process (1 to 12).</param>
    /// <returns>A list of weeks, where each week is a list of <see cref="DateOnly"/> objects representing the days in that week.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="month"/> is not in the range 1 to 12.
    /// </exception>
    public static List<List<DateOnly>> GetWeeksInMonth(int year, int month)
    {
        List<List<DateOnly>> weeks = [];
        DateOnly firstDay = new(year, month, 1);
        DateOnly lastDay = new(year, month, DateTime.DaysInMonth(year, month));

        List<DateOnly> currentWeek = [];

        for (var day = firstDay; day <= lastDay; day = day.AddDays(1))
        {
            if (day.DayOfWeek == DayOfWeek.Sunday && currentWeek.Count > 0)
            {
                weeks.Add(currentWeek);
                currentWeek = [];
            }

            currentWeek.Add(day);

            if (day == lastDay)
            {
                weeks.Add(currentWeek);
            }
        }

        return weeks;
    }

    /// <summary>
    /// Gets a list of months in the current culture, where each month is represented
    /// as a <see cref="CommonLibrary.Models.MonthItem"/> containing the month's index and name.
    /// </summary>
    /// <remarks>
    /// The list excludes any empty month names that may be present in the culture's
    /// month name definitions.
    /// </remarks>
    /// <value>
    /// A list of <see cref="CommonLibrary.Models.MonthItem"/> objects, each representing
    /// a month with its index (1-based) and name.
    /// </value>
    public static List<MonthItem> MonthList =>
        CurrentInfo.MonthNames[..^1]
            .Select((monthName, index) => new MonthItem(index + 1, monthName))
            .ToList();
    
    /// <summary>
    /// Get Month name by month index
    /// </summary>
    public static string MonthName
        => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

    /// <param name="from">The starting date to calculate from.</param>
    extension(DateOnly from)
    {
        /// <summary>
        /// Calculates the next occurrence of the specified day of the week, starting from the given date.
        /// </summary>
        /// <param name="dayOfWeek">The target day of the week to find.</param>
        /// <returns>A <see cref="DateOnly"/> object representing the next occurrence of the specified day of the week.</returns>
        public DateOnly Next(DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
            {
                target += 7;
            }

            return from.AddDays(target - start);
        }

    }
}
