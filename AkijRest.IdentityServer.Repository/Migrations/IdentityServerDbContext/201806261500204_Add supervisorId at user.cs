namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddsupervisorIdatuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SuperVisorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SuperVisorId");
        }
    }
}
