namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignAuthorKeyAdd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropColumn("dbo.Books", "AuthorId", "dbo.Authors");
            //AddColumn("dbo.books","AuthorId", c => c.Int());
            //AlterColumn("dbo.Books", "AuthorId", c => c.Int());
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "AuthorId",cascadeDelete:true);
        }

        public override void Down()
        {
        }
    }
}
