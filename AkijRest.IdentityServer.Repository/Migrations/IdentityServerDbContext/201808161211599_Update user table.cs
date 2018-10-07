namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class Updateusertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Designation", c => c.String());
            AddColumn("dbo.Users", "DateOfJoining", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CurrentAddress", c => c.String());
            AddColumn("dbo.Users", "ParmanentAddress", c => c.String());
            AddColumn("dbo.Users", "Education", c => c.String());
            AddColumn("dbo.Users", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Note");
            DropColumn("dbo.Users", "Education");
            DropColumn("dbo.Users", "ParmanentAddress");
            DropColumn("dbo.Users", "CurrentAddress");
            DropColumn("dbo.Users", "DateOfJoining");
            DropColumn("dbo.Users", "Designation");
        }
    }
}
