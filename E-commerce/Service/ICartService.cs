using E_commerce.Dto;
using E_commerce.Models;

namespace E_commerce.Service
{
    public interface ICartService
    {
        Task<ApiResponse<CartItems>> AddToCart(int productId, int userid);
        Task<List<CartViewDto>> GetCart(int userid);
        Task<bool> RemoveFromCart(int userId, int productId);
        Task<bool> DecrementQuantity(int userid, int productid);
        Task<bool> IncrementQuantity(int userid, int productid);


    }
}
