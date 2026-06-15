namespace ExtensionsLibrary;
public static class TimeSpanExtensions
{
    extension(TimeSpan timeSpan)
    {
        public string Format(bool debug = true) =>
            debug ? 
                $"Hours: {(int)timeSpan.TotalHours} " +
                $"Minutes: {(int)timeSpan.TotalMinutes} " +
                $"Seconds:{timeSpan.Seconds:00} " +
                $"Milliseconds:{timeSpan.Milliseconds:00}" : 
                $"{(int)timeSpan.TotalMinutes}:{timeSpan.Seconds:00}:{timeSpan.Milliseconds:00}";
    }
}
