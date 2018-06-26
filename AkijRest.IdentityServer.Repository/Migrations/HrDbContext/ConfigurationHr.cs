namespace AkijRest.IdentityServer.Repository.Migrations.HrDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationHr : DbMigrationsConfiguration<AkijRest.IdentityServer.Repository.Helpers.DbHelpers.HrDbContext>
    {
        public ConfigurationHr()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\HrDbContext";
        }

        protected override void Seed(AkijRest.IdentityServer.Repository.Helpers.DbHelpers.HrDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
