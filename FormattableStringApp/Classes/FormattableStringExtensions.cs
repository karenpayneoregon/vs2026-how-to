namespace FormattableStringApp.Classes;

/// <summary>
/// Provides a set of extension methods for the <see cref="FormattableStringApp.Classes"/> namespace.
/// </summary>
public static class FormattableStringExtensions
{

    extension(FormattableString sender)
    {
        public int Id()
            => Convert.ToInt32(sender.GetArguments()[0]!.ToString());

        public string FirstName()
            => (string)sender.GetArguments()[1]!;

        public string LastName()
            => (string)sender.GetArguments()[2]!;

        public DateOnly BirthDate()
            => (DateOnly)sender.GetArguments()[3]!;

        public void UpdateFirstName(string value)
        {
            sender.GetArguments()[1] = value;
        }

        public void UpdateLastName(string value)
        {
            sender.GetArguments()[2] = value;
        }

        public void UpdateBirthDate(DateOnly value)
        {
            sender.GetArguments()[3] = value;
        }
    }
}