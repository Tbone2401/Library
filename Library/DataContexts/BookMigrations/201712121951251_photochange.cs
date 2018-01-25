namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photochange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "Book_Id", "dbo.Books");
            DropIndex("dbo.Pictures", new[] { "Book_Id" });
            AddColumn("dbo.Books", "Path", c => c.String(maxLength: 200));
            DropTable("dbo.Pictures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        DisplayName = c.String(),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Books", "Path");
            CreateIndex("dbo.Pictures", "Book_Id");
            AddForeignKey("dbo.Pictures", "Book_Id", "dbo.Books", "Id");
        }
    }
}
