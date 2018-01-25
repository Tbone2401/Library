namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Pages = c.Int(nullable: false),
                        BookAuthor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.BookAuthor_Id, cascadeDelete: true)
                .Index(t => t.BookAuthor_Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "BookAuthor_Id" });
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
        }
    }
}
