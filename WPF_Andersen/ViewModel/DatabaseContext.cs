using Model.Entities;
using System.Data.Entity;

namespace ViewModel
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DbContext")
        { }
        public DbSet<Client> Clients { get; set; }
    }
}
