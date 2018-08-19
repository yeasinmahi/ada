namespace AkijRest.IdentityServer.Repository.Migrations.IdentityServerDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMealtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayName = c.String(nullable: false, maxLength: 10),
                        MenuList = c.String(),
                        AltMenuList = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meals");
        }
    }
}
