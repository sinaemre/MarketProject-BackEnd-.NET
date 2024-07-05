using ApplicationCore.Entities.Concrete;
using DataAccess.Configs;
using DataAccess.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Projeyi çalıştırdığınız sizin yerinize otomatik olarak update-database komutunu çalıştır.
            Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed Data'lar
            modelBuilder.ApplyConfiguration(new CategorySeedData());
            
            //Configler
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
