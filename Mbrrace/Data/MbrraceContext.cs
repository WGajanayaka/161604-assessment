using Mbrrace.Models;
using Microsoft.EntityFrameworkCore;

namespace Mbrrace.Data
{
    public class MbrraceContext : DbContext
    {
        public MbrraceContext (DbContextOptions<MbrraceContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; } = default!;
    }
}
