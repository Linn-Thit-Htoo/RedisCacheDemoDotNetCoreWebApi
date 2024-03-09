using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisCacheDemoDotNetCoreWebApi.Data;
using RedisCacheDemoDotNetCoreWebApi.Models;
using RedisCacheDemoDotNetCoreWebApi.Services;

namespace RedisCacheDemoDotNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICacheService _cacheService;

        public BlogController(AppDbContext appDbContext, ICacheService cacheService)
        {
            _appDbContext = appDbContext;
            _cacheService = cacheService;
        }


        #region Get Blogs
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var cachedData = _cacheService.GetData<IEnumerable<BlogDataModel>>("blogs");
                if (cachedData is not null)
                {
                    return Ok(cachedData);
                }

                var expirationTime = DateTimeOffset.Now.AddMinutes(5);
                var data = await _appDbContext.Blogs
                    .AsNoTracking()
                    .ToListAsync();

                _cacheService.SetData<IEnumerable<BlogDataModel>>("blogs", data, expirationTime);

                return Ok(data);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
