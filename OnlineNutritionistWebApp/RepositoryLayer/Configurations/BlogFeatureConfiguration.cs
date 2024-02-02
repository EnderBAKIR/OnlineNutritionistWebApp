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
    internal class BlogFeatureConfiguration : IEntityTypeConfiguration<BlogFeature>
    {
        public void Configure(EntityTypeBuilder<BlogFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
           
            builder.HasOne(x => x.Blog).WithMany(x=> x.BlogFeature).HasForeignKey(x=>x.BlogId).OnDelete(DeleteBehavior.Cascade);
            //Blog sütunundan nesne silindiği zaman blogfeaturedanda silinsin
            //if deleted item from blog column also delete it from the blogfeature


           builder.HasOne(x => x.AppUser).WithMany(x => x.BlogFeatures).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
