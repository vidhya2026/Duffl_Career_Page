using Duffl_career.Models;
using Microsoft.EntityFrameworkCore;

namespace Duffl_career.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) { }

        public DbSet<Career_Detail> ContactTable { get; set; }
    }
}
