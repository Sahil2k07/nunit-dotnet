using Microsoft.EntityFrameworkCore;
using nunit.models;

namespace nunit.db
{
    public class NunitDbContext : DbContext
    {
        public required DbSet<User> Users { get; set; }

        public NunitDbContext(DbContextOptions<NunitDbContext> options)
            : base(options) { }
    }
}
