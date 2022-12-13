using Mbrrace.Models;
using Microsoft.EntityFrameworkCore;

namespace Mbrrace.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new MbrraceContext(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<MbrraceContext>>()))
                {
                    if (context.Person.Any())
                    {
                        return;   // DB has been seeded
                    }
                    context.Person.AddRange(
                        new Person
                        {
                            GivenName = "Wasantha",
                            FamilyName = "Gajanayaka",
                            Address1 = "307A Uswatta Road",
                            Address2 = "Arawwala",
                            Town = "Pannipitiya",
                            Postcode = "NW10 BJP",
                            Email = "gajanayaka@gmail.com",
                            DateOfBirth = DateTime.Parse("1978-08-14"),
                            CreatedDateTime = DateTime.Now
                        },
                        new Person
                        {
                            GivenName = "Gio",
                            FamilyName = "Parla",
                            Address1 = "Ballogie Ave",
                            Address2 = "Ballogie",
                            Town = "London",
                            Postcode = "NW10 1SU",
                            Email = "gio124@gmail.com",
                            DateOfBirth = DateTime.Parse("1965-01-4"),
                            CreatedDateTime = DateTime.Now
                        }

                    );
                    context.SaveChanges();
                }
            }
            catch
            {
                //Database Error
            }
        }
    }
}
