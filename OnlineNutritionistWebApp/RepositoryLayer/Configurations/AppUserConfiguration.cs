﻿using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        //Key ve varchar değerlerini Configration sınıflarımız ile gerekli entitiylerimizde belirledik. 
        //We have determined the key and varchar values in our necessary entities with our Configuration classes.
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(20);
           
            builder.HasMany(u => u.Blogs)
           .WithOne(b => b.AppUser)
           .HasForeignKey(b => b.AppUserId)
           .OnDelete(DeleteBehavior.Cascade); // veya diğer uygun DeleteBehavior seçenekleri

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.AppUser)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
