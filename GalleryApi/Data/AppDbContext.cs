using GalleryApi.Model;
using Microsoft.EntityFrameworkCore;

namespace GalleryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<GalleryItem> Kenneth_items {  get; set; }
    }
}
