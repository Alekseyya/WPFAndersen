namespace WPF_Andersen.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WPF_Andersen.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WPF_Andersen.DatabaseContext context)
        {
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Clients.AddOrUpdate(u => u.Id,
                new Client { FirstName = "Aleksey", LastName = "Filaman", Age = 25 },
                new Client { FirstName = "Gena", LastName = "Vita", Age = 43 });
            context.SaveChanges();
        }
    }
}
