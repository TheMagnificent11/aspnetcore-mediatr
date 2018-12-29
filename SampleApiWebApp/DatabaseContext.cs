using System.Threading.Tasks;
using EntityManagement;
using Microsoft.EntityFrameworkCore;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp
{
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public int AttachedRepositories { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<T> EntitySet<T>()
            where T : class
        {
            return Set<T>();
        }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }
    }
}
