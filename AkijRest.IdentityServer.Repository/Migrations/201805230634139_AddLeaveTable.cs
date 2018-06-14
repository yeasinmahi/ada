namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLeaveTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
            DropPrimaryKey("dbo.RoleUsers");
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeaveTypeId = c.Byte(nullable: false),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        LeaveCause = c.String(),
                        LeaveAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.UserId);
            
            AddPrimaryKey("dbo.RoleUsers", new[] { "Role_Id", "User_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "UserId", "dbo.Users");
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "UserId" });
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            DropPrimaryKey("dbo.RoleUsers");
            DropTable("dbo.Leaves");
            AddPrimaryKey("dbo.RoleUsers", new[] { "User_Id", "Role_Id" });
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
        }
    }
}
