using AutoMapper;
using E_commerce.Dbcontext;
using E_commerce.Dto;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddCategory(CategoryViewDto categoryViewDto)
        {
            var isExist = await _context.Category.AnyAsync(x => x.Name.ToLower() == categoryViewDto.Name.ToLower());
            if (!isExist)
            {
                var d = _mapper.Map<Category>(categoryViewDto);
                await _context.Category.AddAsync(d);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<List<CategoryViewDto>> ViewCategory()
        {
            var catagories = await _context.Category.ToListAsync();

            if (catagories.Count == 0)
            {
                return new List<CategoryViewDto>();
            }
            return _mapper.Map<List<CategoryViewDto>>(catagories);
        }
        public async Task<bool> RemoveCategory(int id)
        {
            var res = await _context.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (res == null)
            {
                return false;
            }
            else
            {
                _context.Category.Remove(res);
                await _context.SaveChangesAsync();
                return true;
            }
        }



    }
}
