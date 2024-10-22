using GalleryApi.Data;
using GalleryApi.Interfaces;
using GalleryApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace GalleryApi.Repository
{
    public class GalleryService : IGalleryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GalleryService(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GalleryItem>> GetAllGalleryItemsAsync()
        {
            return await _dbContext.Kenneth_items.AsNoTracking().ToListAsync();
        }

        public async Task<GalleryItem> GetGalleryItemByIdAsync(int id)
        {
            return await _dbContext.Kenneth_items.FindAsync(id);
        }

        public async Task<GalleryItem> UploadImageAsync(GalleryItem item)
        {
            
            var imagesFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Images");

         
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

        
            var localPathFile = Path.Combine(imagesFolder, $"{item.FileName}{item.FileExtension}");

           
            using (var stream = new FileStream(localPathFile, FileMode.Create))
            {
                await item.File.CopyToAsync(stream);
            }

           
            var filePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{item.FileName}{item.FileExtension}";

            item.FilePath = filePath;

          
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteGalleryItemAsync(int id)
        {
            var item = await _dbContext.Kenneth_items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _dbContext.Kenneth_items.Remove(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateGalleryItemAsync(GalleryItem item)
        {

            _dbContext.Kenneth_items.Update(item);
            await _dbContext.SaveChangesAsync();

        }
    }
}
