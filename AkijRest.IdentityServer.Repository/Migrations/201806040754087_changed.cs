namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoleGroups", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoleGroups", "Icon");
        }
    }
}
