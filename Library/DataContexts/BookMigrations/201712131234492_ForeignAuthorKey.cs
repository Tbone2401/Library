namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignAuthorKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "BookAuthor_Id" });
            DropColumn("dbo.Books", "BookAuthor_Id");
            DropTable("dbo.Authors");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Books", "BookAuthor_Id", c => c.Int());
            CreateIndex("dbo.Books", "BookAuthor_Id");
            AddForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors", "Id");
        }
    }
}
