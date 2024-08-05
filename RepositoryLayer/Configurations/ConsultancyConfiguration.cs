using CoreLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configurations
{
    internal class ConsultancyConfiguration
    {
        public void Configure(EntityTypeBuilder<Consultancy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.HasOne(c => c.AppUser)
            .WithOne(u => u.Consultancy)
            .HasForeignKey<Consultancy>(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.GetConsultancies)
        .WithOne(g => g.Consultancy)
        .HasForeignKey(g => g.ConsultancyId)
        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
