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
    internal class BooksConfiguration : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.Ignore(x => x.ImageUrl);


            builder.HasMany(x => x.GetBooks)
                .WithOne(x => x.Books)
                .HasForeignKey(x => x.BooksId)
                .OnDelete(DeleteBehavior.Cascade); //Books'un silinmesi halinde ona ait GetBooks' da silinecek. //If the Books is deleted, it will be deleted on its GetBooks.
        }
    }
}
