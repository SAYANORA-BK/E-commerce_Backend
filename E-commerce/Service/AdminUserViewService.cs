using AutoMapper;
using CloudinaryDotNet.Actions;
using E_commerce.Dbcontext;
using E_commerce.Dto;
using E_commerce.Migrations;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Service
{
    public class AdminUserViewService : IAdminUserViewService
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public AdminUserViewService(AppDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
       
            public async Task<List<UserViewDto>> AllUser()
{
    try
    {
      
        var users = await _Context.users
            .Where(u => u.Role != "Admin")
            .ToListAsync();

        var userDtos = new List<UserViewDto>();

        foreach (var user in users)
        {
            var orders = await _Context.Orders
                .Where(o => o.UserId == user.Id)
                .Select(o => new OrderViewDto
                {
                    TransactionId = o.TransactionId,
                    TotalAmount = o.TotalAmount,
                    DeliveryAdrress = o.Address.HouseName,
                    Phone = o.Address.PhoneNumber,
                    OrderDate = o.OrderDate,
                    Items = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        ProductName = _Context.Product
                                        .Where(p => p.ProductId == oi.productId)
                                        .Select(p => p.Title)
                                        .FirstOrDefault(),
                        Quantity = oi.Quantity,
                        TotalPrice = oi.TotalPrice,
                    }).ToList()
                }).ToListAsync();

            var userDto = new UserViewDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                Orders = orders
            };

            userDtos.Add(userDto);
        }

        return userDtos;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}

        public async Task<UserViewDto> GetUserById(int id)
        {
            var user = await _Context.users.SingleOrDefaultAsync(x => x.Id == id);
            
            if (user == null||user.Role!="Admin")
            {
                return null;
            }
            var order = await _Context.Orders.Where(o => o.UserId == id)
            .Select(o => new OrderViewDto
            {
                TransactionId = o.TransactionId,
                TotalAmount = o.TotalAmount,
                DeliveryAdrress = o.Address.HouseName,
                Phone = o.Address.PhoneNumber,
                OrderDate = o.OrderDate,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                     Id = oi.Id,
                     ProductName = _Context.Product.Where(p=>p.ProductId==oi.productId).Select(p=>p.Title).FirstOrDefault(),
                     Quantity = oi.Quantity,
                     TotalPrice = oi.TotalPrice,

                  
                }).ToList()
            }).ToListAsync();

            var userDto = new UserViewDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                Orders = order
            };

            return userDto;
        }

        public async Task<bool> Blockandunblock(int userid)
        {
            var user = await _Context.users.SingleOrDefaultAsync(u => u.Id == userid);
            if (user == null||user.Role =="Admin")
            {
                return false;
            }
            user.IsBlocked = !user.IsBlocked;
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
