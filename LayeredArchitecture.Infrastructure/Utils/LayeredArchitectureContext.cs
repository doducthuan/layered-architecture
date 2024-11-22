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
        public virtual DbSet<Student> Students { get; set; }

        #region Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    id = 1,
                    first_name = "hubert",
                    last_name = "do",
                    birth_day = DateTime.Now,
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
