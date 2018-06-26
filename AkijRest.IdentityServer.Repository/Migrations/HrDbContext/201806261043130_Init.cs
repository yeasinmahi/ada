namespace AkijRest.IdentityServer.Repository.Migrations.HrDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCafeteriaDetails",
                c => new
                    {
                        intRow = c.Int(nullable: false, identity: true),
                        intEnroll = c.Int(),
                        dteMeal = c.DateTime(storeType: "date"),
                        intMealFor = c.Int(),
                        intCountMeal = c.Int(),
                        intSpendMeal = c.Int(),
                        isOwnGuest = c.Boolean(),
                        isPayable = c.Boolean(),
                        strNarration = c.String(maxLength: 250, unicode: false),
                        intActionBy = c.Int(),
                        dteAction = c.DateTime(),
                        ysnMenualEntry = c.Boolean(),
                    })
                .PrimaryKey(t => t.intRow);
            
            CreateTable(
                "dbo.tblCafeteriaRate",
                c => new
                    {
                        intRow = c.Int(nullable: false, identity: true),
                        strGroup = c.String(maxLength: 250, unicode: false),
                        dteFrom = c.DateTime(storeType: "date"),
                        dteTo = c.DateTime(storeType: "date"),
                        monAmount = c.Decimal(precision: 18, scale: 2, storeType: "numeric"),
                        strNarration = c.String(maxLength: 250, unicode: false),
                        intActionBy = c.Int(),
                        dteAction = c.DateTime(),
                    })
                .PrimaryKey(t => t.intRow);
            
            CreateTable(
                "dbo.tblCafeteria",
                c => new
                    {
                        intRow = c.Int(nullable: false, identity: true),
                        intEnroll = c.Int(),
                        intGroup = c.Int(),
                        intType = c.Int(),
                        intMealOption = c.Int(),
                        strNarration = c.String(maxLength: 250, unicode: false),
                        intActionBy = c.Int(),
                        dteAction = c.DateTime(),
                    })
                .PrimaryKey(t => t.intRow);
            
            CreateTable(
                "dbo.tblDay",
                c => new
                    {
                        intDayOffId = c.Int(nullable: false),
                        strDayName = c.String(maxLength: 100, unicode: false),
                        strMenuList = c.String(maxLength: 750, unicode: false),
                        strMenuList0 = c.String(maxLength: 750, unicode: false),
                    })
                .PrimaryKey(t => t.intDayOffId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblDay");
            DropTable("dbo.tblCafeteria");
            DropTable("dbo.tblCafeteriaRate");
            DropTable("dbo.tblCafeteriaDetails");
        }
    }
}
