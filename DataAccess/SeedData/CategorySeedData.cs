using ApplicationCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category
                    {
                        Id = 1,
                        Name = "Manav",
                        Description = "Sebze ve meyve ürünleri bulunur."
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Teknoloji",
                        Description = "Teknolojik ürünler bulunur."
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Şarküteri",
                        Description = "Kahvaltılık ürünler bulunur"
                    }

                );
        }
    }
}
