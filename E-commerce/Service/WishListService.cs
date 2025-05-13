using AutoMapper;
using E_commerce.Dbcontext;
using E_commerce.Dto;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Service
{
    public class WishListService : IWishListService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public WishListService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> AddToWishList(int userid, int productid)
        {
            
            var productExists = await _context.Product.AnyAsync(p => p.ProductId == productid);
            if (!productExists)
            {
                return $"Product with ID {productid} does not exist.";
            }

            var isExist = await _context.WishList
                .FirstOrDefaultAsync(w => w.ProductId == productid && w.UserId == userid);

            if (isExist == null)
            {
                WishListDto wishListDto = new WishListDto()
                {
                    ProductId = productid,
                    UserId = userid,
                };

                var wish = _mapper.Map<WishList>(wishListDto);
                _context.WishList.Add(wish);
                await _context.SaveChangesAsync();

                return "Item added to wish list.";
            }
            else
            {
                return "Item already in the wishlist.";
            }
        }

        public async Task<List<WishListViewDto>> GetWishList(int userId)
        {
            try
            {
                var items = await _context.WishList.Include(p => p.products)
                    .ThenInclude(c => c.category)
                    .Where(c => c.UserId == userId).ToListAsync();

                if (items != null)
                {
                    var product = items.Select(wishlist => new WishListViewDto
                    {
                        Id = wishlist.Id,
                        ProductId = wishlist.products.ProductId,
                        ProductName = wishlist.products.Title,
                        ProductDescription = wishlist.products.Description,
                        Price = wishlist.products.Price,
                        ProductImage = wishlist.products.Image,
                        CategoryName = wishlist.products.category.Name
                    }).ToList();

                    return product;
                }
                else
                {
                    return new List<WishListViewDto>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> RemoveFromWishlist(int userid, int productid)
        {
            var isexist = await _context.WishList.Include(p => p.products).FirstOrDefaultAsync(w => w.ProductId == productid && w.UserId == userid);
            if (isexist != null)
            {

                _context.WishList.Remove(isexist);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
