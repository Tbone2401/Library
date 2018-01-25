namespace Library.DataContexts.BookMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoNameInsteadOfPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PictureName", c => c.String(maxLength: 200));
            DropColumn("dbo.Books", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Path", c => c.String(maxLength: 200));
            DropColumn("dbo.Books", "PictureName");
        }
    }
}
