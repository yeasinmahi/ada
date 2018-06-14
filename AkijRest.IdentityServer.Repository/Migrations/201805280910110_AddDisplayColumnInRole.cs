namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisplayColumnInRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "DisplayName");
        }
    }
}
