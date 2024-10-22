using System.ComponentModel.DataAnnotations.Schema;

namespace GalleryApi.Model
{
    public class GalleryItem
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDiscription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInByte { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
