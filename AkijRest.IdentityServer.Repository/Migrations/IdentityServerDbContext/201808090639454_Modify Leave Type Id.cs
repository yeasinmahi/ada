namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyLeaveTypeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leaves", "LeaveType_Id", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "LeaveType_Id" });
            DropColumn("dbo.Leaves", "LeaveTypeId");
            RenameColumn(table: "dbo.Leaves", name: "LeaveType_Id", newName: "LeaveTypeId");
            DropPrimaryKey("dbo.LeaveTypes");
            AlterColumn("dbo.Leaves", "LeaveTypeId", c => c.Byte(nullable: false));
            AlterColumn("dbo.LeaveTypes", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.LeaveTypes", "Id");
            CreateIndex("dbo.Leaves", "LeaveTypeId");
            AddForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            DropPrimaryKey("dbo.LeaveTypes");
            AlterColumn("dbo.LeaveTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Leaves", "LeaveTypeId", c => c.Int());
            AddPrimaryKey("dbo.LeaveTypes", "Id");
            RenameColumn(table: "dbo.Leaves", name: "LeaveTypeId", newName: "LeaveType_Id");
            AddColumn("dbo.Leaves", "LeaveTypeId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Leaves", "LeaveType_Id");
            AddForeignKey("dbo.Leaves", "LeaveType_Id", "dbo.LeaveTypes", "Id");
        }
    }
}
