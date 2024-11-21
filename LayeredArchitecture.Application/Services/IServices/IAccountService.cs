using LayeredArchitecture.Application.DTO;
using LayeredArchitecture.Common.ApiResponse;

namespace LayeredArchitecture.Application.Services.IServices
{
    public interface IAccountService
    {
        Task<ApiResponse> GetList();
        Task<ApiResponse> CreateOrUpdate(AccountDTO dto, bool isCreated = true);
        Task<ApiResponse> GetById(int id);
    }
}
