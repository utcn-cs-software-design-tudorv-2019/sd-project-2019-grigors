namespace TellYourFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Edition", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Edition", c => c.Int(nullable: false));
        }
    }
}
