using LayeredArchitecture.Application.DTO;
using LayeredArchitecture.Common.ApiResponse;

namespace LayeredArchitecture.Application.Services.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse> GetList();
        Task<ApiResponse> CreateOrUpdate(StudentDTO dto, bool isCreated = true);
        Task<ApiResponse> GetById(int id);
    }
}
