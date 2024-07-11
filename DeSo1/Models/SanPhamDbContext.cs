using Microsoft.EntityFrameworkCore;

namespace DeSo1.Models
{
    public class SanPhamDbContext : DbContext
    {
        public SanPhamDbContext(DbContextOptions<SanPhamDbContext> options) : base(options)
        {
        }

        public DbSet<SanPham> SanPhams { get; set;}
    }
}
