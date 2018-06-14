namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TokenContent = c.String(),
                        TimeExpiry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tokens");
        }
    }
}
