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
    internal class GetConsultancyConfiguration: IEntityTypeConfiguration<GetConsultancy>
    {
        public void Configure(EntityTypeBuilder<GetConsultancy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.AppUser)
         .WithMany(x => x.GetConsultancies)
         .HasForeignKey(x => x.AppuserId)
         .OnDelete(DeleteBehavior.NoAction);



        }

        
    }
}
