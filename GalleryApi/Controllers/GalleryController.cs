using GalleryApi.Data;
using GalleryApi.Interfaces;
using GalleryApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace GalleryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext dbContext;

        public GalleryController(IGalleryService galleryService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _galleryService = galleryService;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryItems()
        {
            var galleryItems = await _galleryService.GetAllGalleryItemsAsync();
            return Ok(galleryItems);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetGalleryItemById(int id)
        {
            var item = await _galleryService.GetGalleryItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

  
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] UploadFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var imageDomainModel = new GalleryItem
                {
                    File = imageFile.File,
                    FileName = imageFile.FileName,
                    FileExtension = Path.GetExtension(imageFile.File.FileName),
                    FileDiscription = imageFile.FileDiscription,
                    FileSizeInByte = imageFile.File.Length

                };

                await _galleryService.UploadImageAsync(imageDomainModel);
                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGalleryItem(int id, [FromForm] UploadFile imageFile)
        {

            var existingItem = await dbContext.Kenneth_items.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound(); 
            }

            
            if (ModelState.IsValid)
            {
                
                existingItem.FileName = imageFile.FileName;
                existingItem.FileDiscription = imageFile.FileDiscription;
                existingItem.FileSizeInByte = imageFile.File.Length;

                
                if (imageFile.File != null && imageFile.File.Length > 0)
                {
                    existingItem.FileExtension = Path.GetExtension(imageFile.File.FileName);
                    var localPathFile = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{existingItem.FileName}{existingItem.FileExtension}");

                    
                    using var stream = new FileStream(localPathFile, FileMode.Create);
                    await imageFile.File.CopyToAsync(stream);

                   
                    existingItem.FilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{existingItem.FileName}{existingItem.FileExtension}";
                }

                
                await _galleryService.UpdateGalleryItemAsync(existingItem);
                return Ok(existingItem);
            }

            return BadRequest(ModelState);


        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalleryItem(int id)
        {
            var result = await _galleryService.DeleteGalleryItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
