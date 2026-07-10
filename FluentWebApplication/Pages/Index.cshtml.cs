using FluentWebApplication.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace FluentWebApplication.Pages;
public class IndexModel(Context context, IWebHostEnvironment env) : PageModel
{

    public void OnGet()
    {
        if (env.IsDevelopment())
        {
            Console.WriteLine("Running in dev environment"); 
            Log.Information("In development");
        }
    }

    private void UpdateEmailAddresses()
    {
        var people = context.Person.ToList();
        foreach (var (index, person) in people.Index())
        {
            person.EmailAddress = index % 2 == 0 ? 
                $"{person.LastName}{person.FirstName}@gmail.com" : 
                $"{person.FirstName}{person.LastName}@yahoo.com";
        }

        var count = context.SaveChanges();
    }
}
