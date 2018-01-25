namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGenreAsNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "StringsAsString", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "StringsAsString", c => c.String(nullable: false));
        }
    }
}
