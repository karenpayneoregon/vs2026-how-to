namespace FormattableStringApp.Classes;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public void ForEach(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(action);

            foreach (T item in source)
            {
                action(item);
            }
        }
    }
}