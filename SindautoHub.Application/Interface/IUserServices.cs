
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Dtos.UserDtos;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Domain.Interface
{
    public interface IUserServices
    {
       Task<UserResponse> CreateAsync(CreateUserRequest request);

        Task<UserResponse> UpdateAsync(Guid Id , UpdateUserRequest request);

        Task<UserResponse> GetByIdAsync (Guid Id);

       Task<List<UserResponse>> GetAllAsync();

        Task<List<UserBySectorResponse>> GetUsersBySectorAsync(Guid sectorId);

        Task<bool> DeleteAsync(Guid Id);

    }
}
