namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateLeaveTypeTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO LeaveTypes(Id,Name) Values(1,'Casual Leave')");
            Sql("INSERT INTO LeaveTypes(Id,Name) Values(2,'Medical Leave')");
            Sql("INSERT INTO LeaveTypes(Id,Name) Values(3,'Privilege Leave')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM LeaveTypes");
        }
    }
}
