using Microsoft.EntityFrameworkCore;
using MyBlog.App.Models;

namespace MyBlog.App.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithOne(u => u.Favorite)
                .HasForeignKey<Favorite>(f => f.UserId);
            //modelBuilder.Entity<FavoriteItem>()
            //    .HasOne<Favorite>(fi => fi.Favorite)
            //    .WithMany(f => f.FavoriteItems)
            //    .HasForeignKey(fi => fi.FavoriteId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
