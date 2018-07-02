namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicableForType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeaveTypes", "ApplicableFor", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeaveTypes", "ApplicableFor");
        }
    }
}
