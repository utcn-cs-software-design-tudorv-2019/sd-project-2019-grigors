namespace TellYourFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Description = c.String(),
                        Edition = c.String(),
                        Image = c.String(),
                        Rating = c.Double(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Name = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        Director = c.String(),
                        Image = c.String(),
                        Rating = c.Double(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Movie_Id = c.Int(),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.Movie_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryBooks",
                c => new
                    {
                        Category_Name = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Name, t.Book_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Name, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Category_Name)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.MovieCategories",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Category_Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Category_Name })
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Name, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Category_Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Books", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieCategories", "Category_Name", "dbo.Categories");
            DropForeignKey("dbo.MovieCategories", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CategoryBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.CategoryBooks", "Category_Name", "dbo.Categories");
            DropIndex("dbo.MovieCategories", new[] { "Category_Name" });
            DropIndex("dbo.MovieCategories", new[] { "Movie_Id" });
            DropIndex("dbo.CategoryBooks", new[] { "Book_Id" });
            DropIndex("dbo.CategoryBooks", new[] { "Category_Name" });
            DropIndex("dbo.Comments", new[] { "Book_Id" });
            DropIndex("dbo.Comments", new[] { "Movie_Id" });
            DropIndex("dbo.Movies", new[] { "User_Id" });
            DropIndex("dbo.Books", new[] { "User_Id" });
            DropTable("dbo.MovieCategories");
            DropTable("dbo.CategoryBooks");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Movies");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
        }
    }
}
