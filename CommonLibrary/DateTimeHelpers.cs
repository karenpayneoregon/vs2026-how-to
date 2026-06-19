using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CommonLibrary;

/// <summary>
/// Provides a collection of helper methods for working with dates, including operations
/// such as retrieving dates for the next week, dividing a month into weeks, and more.
/// </summary>
public static class DateTimeHelpers
{

    /// <summary>
    /// Retrieves the dates for the prior week starting from the previous occurrence of Sunday.
    /// </summary>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the prior week.</returns>
    public static List<DateOnly> PriorWeeksDates()
        => Enumerable.Range(0, 7).Select(index =>
                DateOnly.FromDateTime(DateTime.Now).Previous(DayOfWeek.Sunday).AddDays(index))
            .ToList();
    
    /// <summary>
    /// Retrieves the dates for the next week starting from the next occurrence of Sunday.
    /// </summary>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the next week.</returns>
    public static List<DateOnly> NextWeeksDates()
        => Enumerable.Range(0, 7).Select(index =>
                DateOnly.FromDateTime(DateTime.Now).Next(DayOfWeek.Sunday).AddDays(index))
            .ToList();

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

        /// <summary>
        /// Calculates the previous occurrence of the specified day of the week, starting from the given date.
        /// </summary>
        /// <param name="dayOfWeek">The target day of the week to find.</param>
        /// <returns>A <see cref="DateOnly"/> object representing the previous occurrence of the specified day of the week.</returns>
        public DateOnly Previous(DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target >= start)
            {
                target -= 7;
            }

            return from.AddDays(target - start);
        }
    }
}
