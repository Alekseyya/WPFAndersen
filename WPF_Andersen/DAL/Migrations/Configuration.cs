using Model.Entities;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.DatabaseContext context)
        {
            context.Clients.AddOrUpdate(u => u.Id,
                new Client { FirstName = "Aleksey", LastName = "Filaman", Age = 25 },
                new Client { FirstName = "Gena", LastName = "Vita", Age = 43 });
            context.SaveChanges();
        }
    }
}
