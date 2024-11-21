using LayeredArchitecture.Application.Services.IServices;
using LayeredArchitecture.Domain.IReponsitories;
using LayeredArchitecture.Common.ApiResponse;
using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Application.Form;
using LayeredArchitecture.Application.DTO;
using LayeredArchitecture.Domain.Models;
namespace LayeredArchitecture.Application.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;
        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse> GetList()
        {
            var data = await _repository
                .FindByCondition(x => x.delete_flg != true)
                .Select(x => new AccountForm
                {
                    id = x.id,
                    user_name = x.user_name,
                    created_at = x.created_at,
                    updated_at = x.updated_at
                })
                .ToListAsync();
            return data != null ? 
                ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_200_01, data: data) :
                ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_404_01);
        }
        public async Task<ApiResponse> GetById(int id)
        {
            var data = await _repository
                .FindByCondition(x => x.id == id && x.delete_flg != true)
                .Select(x => new AccountForm
                {
                    id = x.id,
                    user_name = x.user_name,
                    created_at = x.created_at,
                    updated_at = x.updated_at
                })
                .FirstOrDefaultAsync();
            return data != null ? 
                ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_200_01, data: data):
                ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_404_01);
        }
        public async Task<ApiResponse> CreateOrUpdate(AccountDTO dto, bool isCreated = true)
        {
            var model = isCreated ? new Account() : _repository.GetById(dto.id) ?? new Account();
            if(!isCreated && model.id <= 0)
            {
                return ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_400_01);
            }
            SetValueModelByDto(dto, ref model);
            if((isCreated && (await _repository.CreateAsync(model)) <= 0) ||
                (!isCreated && (await _repository.UpdateAsync(model)))){
                return ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_400_01);
            }
            return ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_200_01, data: model);
        }
        private Account SetValueModelByDto(AccountDTO dto, ref Account model)
        {
            model.user_name = dto.user_name;
            model.password = dto.password;
            return model;
        }
    }
}
