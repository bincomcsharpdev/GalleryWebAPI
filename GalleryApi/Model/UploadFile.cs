using System.ComponentModel.DataAnnotations;

namespace GalleryApi.Model
{
    public class UploadFile
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDiscription { get; set; }
    }

    public class UpdateUploadFile
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDiscription { get; set; }
    }
}
