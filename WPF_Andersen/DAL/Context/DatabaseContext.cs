using Model.Entities;
using System.Data.Entity;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DbContext")
        { }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
        }

        public class DatabaseInitializer
            : CreateDatabaseIfNotExists<DatabaseContext>
        {
            public override void InitializeDatabase(DatabaseContext context)
            {

            }
        }
    }
}
