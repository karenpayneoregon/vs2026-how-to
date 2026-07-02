using HasConversion_Bool_ColorApp.Data;
using HasConversion_Bool_ColorApp.Models;

using static HasConversion_Bool_ColorApp.Classes.Helpers;

using Color = System.Drawing.Color;


namespace HasConversion_Bool_ColorApp.Classes;

internal class MockData
{
    /// <summary>
    /// Initializes the database with fresh data by clearing existing data and adding predefined records.
    /// </summary>
    /// <param name="context">
    /// The <see cref="Context"/> instance used to interact with the database.
    /// </param>
    /// <remarks>
    /// This method removes all existing data from the database and inserts a predefined set of 
    /// <see cref="Person"/> records with specific attributes, such as names, birthdates, friendship status, 
    /// and colors. Changes are saved to the database.
    /// </remarks>
    public static void StartFresh(Context context)
    {
        CleanDatabase(context);


        context.Add(new Person()
        {
            FirstName = "Jim",
            LastName = "Jacobe",
            IsFriend = true,
            DateTime = new DateTime(2022, 5, 5),
            BirthDate = new DateOnly(1943, 2, 2),
            Color = Color.Green
        });

        context.Add(new Person()
        {
            FirstName = "Bob",
            LastName = "Smith",
            BirthDate = new DateOnly(1966, 12, 5),
            IsFriend = false,
            Color = Color.Yellow
        });

        context.Add(new Person()
        {
            FirstName = "Karen",
            LastName = "Payne",
            BirthDate = new DateOnly(1956, 9, 22),
            IsFriend = true,
            Color = Color.Red
        });

        context.SaveChanges();
    }
}
