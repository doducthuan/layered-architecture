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
    public class StudentController : BaseController
    {
        private readonly IStudentService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<StudentController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKey = "cacheAccount";
        public StudentController(
            IStudentService service,
            IHttpContextAccessor httpContextAccessor,
            ILogger<StudentController> logger,
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
            if (!_memoryCache.TryGetValue(cacheKey, out ApiResponse? data))
            {
                //var cacheOptions = new MemoryCacheEntryOptions
                //{
                //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                //    SlidingExpiration = TimeSpan.FromMinutes(5)
                //};
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
        public async Task<IActionResult> CreateOrUpdate(StudentDTO dto)
        {
            var result = await _service.CreateOrUpdate(dto, dto.id <= 0);
            if(result.code == DefineResponse.EnumCodes.R_CMN_200_01.ToString().Split('_')[2])
            {
                _memoryCache.Remove(cacheKey);
            }
            return BaseResult(result);
        }
    }
}
