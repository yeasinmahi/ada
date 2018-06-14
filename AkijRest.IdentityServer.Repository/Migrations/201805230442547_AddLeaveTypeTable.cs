namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLeaveTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LeaveTypes");
        }
    }
}
