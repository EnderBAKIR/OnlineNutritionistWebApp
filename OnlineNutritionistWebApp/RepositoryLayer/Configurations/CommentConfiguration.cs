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
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
     
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.AppUserName).IsRequired(); //Null olmasın ve mutlaka zorunlu olsun. 
            builder.Property(x => x.AppUserSurname).IsRequired(); //It should not be null and it should be mandatory.
            //builder.Property(x => x.AppUser).IsRequired();
            builder.Property(x => x.AppUserImageUrl).IsRequired();
            builder.Property(x => x.CommentContent).IsRequired();


            builder.HasOne(x => x.AppUser)
                   .WithMany()
                   .HasForeignKey(x => x.AppUserId)
                   .IsRequired();

        }
    }
}
