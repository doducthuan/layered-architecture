using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Domain.Models;

namespace LayeredArchitecture.Infrastructure.Utils
{
    public class LayeredArchitectureContext : DbContext
    {
        public LayeredArchitectureContext(DbContextOptions<LayeredArchitectureContext> options) : base(options)
        {
        }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
