using Microsoft.AspNetCore.Mvc;
using LayeredArchitecture.Application.Services.IServices;
using LayeredArchitecture.Application.DTO;
using LayeredArchitecture.Common.ApiResponse;
namespace LayeredArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountController> _logger;
        public AccountController(
            IAccountService service,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AccountController> logger)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var data = await _service.GetList();
            return BaseResult(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetById(id);
            return BaseResult(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(AccountDTO dto)
        {           
            var result = await _service.CreateOrUpdate(dto, dto.id <= 0);
            return BaseResult(result);
        }
    }
}
