using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace ByteWriter.Models
{
    public class BloggingContext : IdentityDbContext<User>
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<TrendingBlog> TrendingBlogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Attachment)
                .WithOne(a => a.Article)
                .HasForeignKey<Article>(a => a.AttachmentId)
                .OnDelete(DeleteBehavior.SetNull);  

            modelBuilder.Entity<Attachment>()
                .HasOne(a => a.Article)
                .WithOne(a => a.Attachment)
                .HasForeignKey<Attachment>(a => a.ArticleId)
                .OnDelete(DeleteBehavior.SetNull);  

            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne()
                .HasForeignKey<User>(u => u.BlogId)
                .OnDelete(DeleteBehavior.SetNull);  
        }
    }
}
