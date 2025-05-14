using E_commerce.Dto;

namespace E_commerce.Service
{
    public interface IAdminUserViewService
    {

        Task<List<UserViewDto>> AllUser();
        Task<UserViewDto> GetUserById(int id);
        Task<bool> Blockandunblock(int userid);
    }
}
