namespace AkijRest.IdentityServer.Repository.Migrations.GlobalDbContext
{
    using System.Data.Entity.Migrations;

    internal sealed class ConfigurationGlobal : DbMigrationsConfiguration<AkijRest.IdentityServer.Repository.Helpers.DbHelpers.GlobalDbContext>
    {
        public ConfigurationGlobal()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\GlobalDbContext";
        }

        protected override void Seed(AkijRest.IdentityServer.Repository.Helpers.DbHelpers.GlobalDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
