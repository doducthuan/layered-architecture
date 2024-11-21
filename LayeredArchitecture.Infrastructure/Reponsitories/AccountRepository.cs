using caresystem_data_bussiness.Repository;
using LayeredArchitecture.Domain.IReponsitories;
using LayeredArchitecture.Domain.Models;
using Microsoft.AspNetCore.Http;
using LayeredArchitecture.Infrastructure.Utils;

namespace LayeredArchitecture.Infrastructure.Reponsitories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private IHttpContextAccessor _httpContextAccessor;
        private LayeredArchitectureContext _context;
        public AccountRepository(LayeredArchitectureContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
    }
}
