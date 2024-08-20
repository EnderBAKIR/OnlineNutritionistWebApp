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
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Price).IsRequired();
            builder.Ignore(x => x.ImageUrl);

            builder.HasOne(x => x.AppUser).WithMany(x => x.Packages).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
