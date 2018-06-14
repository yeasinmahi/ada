namespace AkijRest.IdentityServer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExternalLoginEmailTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExternalLoginEmails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Google = c.String(),
                        Facebook = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExternalLoginEmails", "Id", "dbo.Users");
            DropIndex("dbo.ExternalLoginEmails", new[] { "Id" });
            DropTable("dbo.ExternalLoginEmails");
        }
    }
}
