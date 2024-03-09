using Microsoft.EntityFrameworkCore;
using RedisCacheDemoDotNetCoreWebApi.Models;

namespace RedisCacheDemoDotNetCoreWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
