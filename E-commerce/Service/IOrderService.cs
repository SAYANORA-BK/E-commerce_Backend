using E_commerce.Dto;
using E_commerce.Models;

namespace E_commerce.Service
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(int userid, CreateOrderDto addorder);
        Task<ApiResponse<List<OrderViewDto>>> GetOrders(int userid);
        Task<List<AdminViewOrderDto>> GetOrdersforAdmin(int userid);
        Task<int> TotalProductSold();
        Task<decimal?> TotalRevenue();
        Task<ApiResponse<string>> UpdateOrderStatus(int orderId, string newStatus);
    }
}
