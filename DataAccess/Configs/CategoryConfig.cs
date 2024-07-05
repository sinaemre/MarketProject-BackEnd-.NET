using ApplicationCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category"); //Tablo ismi belirleme

            builder.HasKey(x => x.Id); //Primary Key Belirler

            //Tablo kuralları belirleme
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(750).IsRequired(true);
        }
    }
}
