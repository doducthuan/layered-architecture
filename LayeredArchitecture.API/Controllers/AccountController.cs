using Microsoft.AspNetCore.Mvc;
using LayeredArchitecture.Application.Services.IServices;
using LayeredArchitecture.Application.DTO;
using LayeredArchitecture.Common.ApiResponse;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
namespace LayeredArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountController> _logger;
        private readonly IMemoryCache _memoryCache;
        public AccountController(
            IAccountService service,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AccountController> logger,
            IMemoryCache memoryCache)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var stopWatch = Stopwatch.StartNew();
            string cacheKey = "cachList";
            if (!_memoryCache.TryGetValue(cacheKey, out ApiResponse? data))
            {
                data = await _service.GetList();
                _memoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(10));
            }
            //var data = await _service.GetList();
            stopWatch.Stop();
            var timeTaken = stopWatch.ElapsedMilliseconds;
            Console.WriteLine($"API executed in {timeTaken} ms");

            
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
