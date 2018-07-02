namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyleavetypemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "LeaveTypeId" });
            RenameColumn(table: "dbo.LeaveTypes", name: "Name", newName: "LeaveTypeName");
            DropPrimaryKey("dbo.LeaveTypes");
            AddColumn("dbo.Leaves", "LeaveType_Id", c => c.Int());
            AddColumn("dbo.LeaveTypes", "CompanyPolicy", c => c.Int(nullable: false));
            AddColumn("dbo.LeaveTypes", "MaximumAllowedAtATime", c => c.Int(nullable: false));
            AddColumn("dbo.LeaveTypes", "IsHalfDayAllowed", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeaveTypes", "IsBalanceChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeaveTypes", "IsOnlyOneTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeaveTypes", "MaxApplicationAtAMonth", c => c.Int(nullable: false));
            AddColumn("dbo.LeaveTypes", "IsRestricted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LeaveTypes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.LeaveTypes", "LeaveTypeName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.LeaveTypes", "Id");
            CreateIndex("dbo.Leaves", "LeaveType_Id");
            AddForeignKey("dbo.Leaves", "LeaveType_Id", "dbo.LeaveTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "LeaveType_Id", "dbo.LeaveTypes");
            DropIndex("dbo.Leaves", new[] { "LeaveType_Id" });
            DropPrimaryKey("dbo.LeaveTypes");
            AlterColumn("dbo.LeaveTypes", "LeaveTypeName", c => c.String());
            AlterColumn("dbo.LeaveTypes", "Id", c => c.Byte(nullable: false));
            DropColumn("dbo.LeaveTypes", "IsRestricted");
            DropColumn("dbo.LeaveTypes", "MaxApplicationAtAMonth");
            DropColumn("dbo.LeaveTypes", "IsOnlyOneTime");
            DropColumn("dbo.LeaveTypes", "IsBalanceChecked");
            DropColumn("dbo.LeaveTypes", "IsHalfDayAllowed");
            DropColumn("dbo.LeaveTypes", "MaximumAllowedAtATime");
            DropColumn("dbo.LeaveTypes", "CompanyPolicy");
            DropColumn("dbo.Leaves", "LeaveType_Id");
            AddPrimaryKey("dbo.LeaveTypes", "Id");
            RenameColumn(table: "dbo.LeaveTypes", name: "LeaveTypeName", newName: "Name");
            CreateIndex("dbo.Leaves", "LeaveTypeId");
            AddForeignKey("dbo.Leaves", "LeaveTypeId", "dbo.LeaveTypes", "Id", cascadeDelete: true);
        }
    }
}
