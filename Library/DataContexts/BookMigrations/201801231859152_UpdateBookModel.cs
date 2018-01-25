namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "StringsAsString", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "StringsAsString");
        }
    }
}
