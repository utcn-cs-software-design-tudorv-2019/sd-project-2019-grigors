namespace TellYourFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Comments", "Book_Id", "dbo.Books");
            DropIndex("dbo.Comments", new[] { "Movie_Id" });
            DropIndex("dbo.Comments", new[] { "Book_Id" });
            CreateTable(
                "dbo.CommentBooks",
                c => new
                    {
                        Comment_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_Id, t.Book_Id })
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Comment_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.CommentMovies",
                c => new
                    {
                        Comment_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_Id, t.Movie_Id })
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Comment_Id)
                .Index(t => t.Movie_Id);
            
            DropColumn("dbo.Comments", "Movie_Id");
            DropColumn("dbo.Comments", "Book_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Book_Id", c => c.Int());
            AddColumn("dbo.Comments", "Movie_Id", c => c.Int());
            DropForeignKey("dbo.CommentMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CommentMovies", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.CommentBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.CommentBooks", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.CommentMovies", new[] { "Movie_Id" });
            DropIndex("dbo.CommentMovies", new[] { "Comment_Id" });
            DropIndex("dbo.CommentBooks", new[] { "Book_Id" });
            DropIndex("dbo.CommentBooks", new[] { "Comment_Id" });
            DropTable("dbo.CommentMovies");
            DropTable("dbo.CommentBooks");
            CreateIndex("dbo.Comments", "Book_Id");
            CreateIndex("dbo.Comments", "Movie_Id");
            AddForeignKey("dbo.Comments", "Book_Id", "dbo.Books", "Id");
            AddForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies", "Id");
        }
    }
}
