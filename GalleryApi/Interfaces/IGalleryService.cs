using GalleryApi.Model;

namespace GalleryApi.Interfaces
{
    public interface IGalleryService
    {
        Task<List<GalleryItem>> GetAllGalleryItemsAsync();
        Task<GalleryItem> GetGalleryItemByIdAsync(int id);
        Task<GalleryItem> UploadImageAsync(GalleryItem item);
        Task<bool> DeleteGalleryItemAsync(int id);
        Task UpdateGalleryItemAsync(GalleryItem item);
    }
}
