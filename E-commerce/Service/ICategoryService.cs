using E_commerce.Dto;

namespace E_commerce.Service
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CategoryViewDto categoryViewDto);
        Task<List<CategoryViewDto>> ViewCategory();
        Task<bool> RemoveCategory(int id);
    }
}
