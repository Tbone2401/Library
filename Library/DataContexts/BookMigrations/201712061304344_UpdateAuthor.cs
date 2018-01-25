namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAuthor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "BookAuthor_Id" });
            AlterColumn("dbo.Books", "BookAuthor_Id", c => c.Int());
            CreateIndex("dbo.Books", "BookAuthor_Id");
            AddForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "BookAuthor_Id" });
            AlterColumn("dbo.Books", "BookAuthor_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "BookAuthor_Id");
            AddForeignKey("dbo.Books", "BookAuthor_Id", "dbo.Authors", "Id", cascadeDelete: true);
        }
    }
}
