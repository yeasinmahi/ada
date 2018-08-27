namespace AkijRest.IdentityServer.Repository.Migrations.GlobalDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTaskRecord",
                c => new
                    {
                        intID = c.Int(nullable: false, identity: true),
                        intEnroll = c.Int(nullable: false),
                        dteDate = c.DateTime(nullable: false, storeType: "date"),
                        strKeyPoint = c.String(nullable: false, maxLength: 500, unicode: false),
                        strStatus = c.String(nullable: false, maxLength: 500, unicode: false),
                        strRemarks = c.String(nullable: false, maxLength: 500, unicode: false),
                        dteStartTime = c.DateTime(nullable: false),
                        dteEndTime = c.DateTime(nullable: false),
                        dteDeadLine = c.DateTime(nullable: false),
                        intComplete = c.Int(nullable: false),
                        strType = c.String(nullable: false, maxLength: 50, unicode: false),
                        dteEmailSend = c.DateTime(),
                    })
                .PrimaryKey(t => t.intID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblTaskRecord");
        }
    }
}
