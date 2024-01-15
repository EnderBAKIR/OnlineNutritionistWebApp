using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class GetBooksConfiguration : IEntityTypeConfiguration<GetBooks>
    {
        public void Configure(EntityTypeBuilder<GetBooks> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).IsRequired();


            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.GetBooks)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
