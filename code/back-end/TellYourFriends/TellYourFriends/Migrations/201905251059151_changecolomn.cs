namespace TellYourFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolomn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Edition", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Author", c => c.String());
            DropColumn("dbo.Movies", "Year");
            DropColumn("dbo.Movies", "Director");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Director", c => c.String());
            AddColumn("dbo.Movies", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Author");
            DropColumn("dbo.Movies", "Edition");
        }
    }
}
