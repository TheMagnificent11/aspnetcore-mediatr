using System.Threading.Tasks;
using EntityManagement;
using Microsoft.EntityFrameworkCore;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp
{
    /// <summary>
    /// Database Context
    /// </summary>
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class
        /// </summary>
        /// <param name="options">Database context options</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the number of attached repositories
        /// </summary>
        public int AttachedRepositories { get; set; }

        /// <summary>
        /// Gets or sets the teams set
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Gets or sets the players set
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets the entity set for the specified entity type
        /// </summary>
        /// <typeparam name="T">Entity type of database set</typeparam>
        /// <returns>Database set</returns>
        public DbSet<T> EntitySet<T>()
            where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database
        /// </summary>
        /// <returns>The number of state entries written to the underlying database</returns>
        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }
    }
}
