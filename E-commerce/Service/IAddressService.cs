using E_commerce.Dto;
using E_commerce.Models;

namespace E_commerce.Service
{
    public interface IAddressService
    {
        Task<ApiResponse<string>> AddAdress(int userid, CreateAddressDto addaddress);
        Task<ApiResponse<List<ShowAddressDto>>> ShowAddresses(int userid);
        Task<bool> DeleteAddress(int userid, int addressid);
    }
}
