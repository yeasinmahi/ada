namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContollerActionAndIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Controller", c => c.String());
            AddColumn("dbo.Roles", "Action", c => c.String());
            AddColumn("dbo.Roles", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "Icon");
            DropColumn("dbo.Roles", "Action");
            DropColumn("dbo.Roles", "Controller");
        }
    }
}
