namespace E_commerce.Service
{
    public interface ICloudinaryservice
    {
        Task<string> UploadImage(IFormFile file);
    }
}
