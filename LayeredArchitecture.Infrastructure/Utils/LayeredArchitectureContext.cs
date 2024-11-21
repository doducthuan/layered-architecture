using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace LayeredArchitecture.Infrastructure.Utils
{
    public class LayeredArchitectureContext : DbContext
    {
        public LayeredArchitectureContext(DbContextOptions<LayeredArchitectureContext> options) : base(options)
        {
        }      
        public virtual DbSet<Account> Accounts { get; set; }

        #region Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    id = 1,
                    user_name = "test1",
                    password = "password",
                    created_at = DateTime.Now,
                    created_user = "1",
                    delete_flg = false,
                }
            );
            base.OnModelCreating(modelBuilder);
        }      
        #endregion
    }
}
