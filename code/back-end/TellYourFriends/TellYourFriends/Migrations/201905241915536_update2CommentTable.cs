namespace TellYourFriends.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2CommentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentBooks", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.CommentBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.CommentMovies", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.CommentMovies", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.CommentBooks", new[] { "Comment_Id" });
            DropIndex("dbo.CommentBooks", new[] { "Book_Id" });
            DropIndex("dbo.CommentMovies", new[] { "Comment_Id" });
            DropIndex("dbo.CommentMovies", new[] { "Movie_Id" });
            AddColumn("dbo.Comments", "Book_Id", c => c.Int());
            AddColumn("dbo.Comments", "Movie_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Book_Id");
            CreateIndex("dbo.Comments", "Movie_Id");
            AddForeignKey("dbo.Comments", "Book_Id", "dbo.Books", "Id");
            AddForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies", "Id");
            DropTable("dbo.CommentBooks");
            DropTable("dbo.CommentMovies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommentMovies",
                c => new
                    {
                        Comment_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_Id, t.Movie_Id });
            
            CreateTable(
                "dbo.CommentBooks",
                c => new
                    {
                        Comment_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_Id, t.Book_Id });
            
            DropForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Comments", "Book_Id", "dbo.Books");
            DropIndex("dbo.Comments", new[] { "Movie_Id" });
            DropIndex("dbo.Comments", new[] { "Book_Id" });
            DropColumn("dbo.Comments", "Movie_Id");
            DropColumn("dbo.Comments", "Book_Id");
            CreateIndex("dbo.CommentMovies", "Movie_Id");
            CreateIndex("dbo.CommentMovies", "Comment_Id");
            CreateIndex("dbo.CommentBooks", "Book_Id");
            CreateIndex("dbo.CommentBooks", "Comment_Id");
            AddForeignKey("dbo.CommentMovies", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentMovies", "Comment_Id", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentBooks", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentBooks", "Comment_Id", "dbo.Comments", "Id", cascadeDelete: true);
        }
    }
}
