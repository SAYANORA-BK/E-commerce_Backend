using AutoMapper;
using E_commerce.Dbcontext;
using E_commerce.Dto;
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

                if (users.Count > 0)
                {
                    var userDtos = _mapper.Map<List<UserViewDto>>(users);
                    return userDtos;
                }

                return new List<UserViewDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserViewDto> GetUserById(int id)
        {
            var user = await _Context.users
                .SingleOrDefaultAsync(x => x.Id == id && x.Role != "Admin");

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserViewDto>(user);
        }

        public async Task<bool> Blockandunblock(int userid)
        {
            var user = await _Context.users.SingleOrDefaultAsync(u => u.Id == userid);

            if (user == null || user.Role == "Admin") 
            {
                return false;
            }

            user.IsBlocked = !user.IsBlocked;
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
