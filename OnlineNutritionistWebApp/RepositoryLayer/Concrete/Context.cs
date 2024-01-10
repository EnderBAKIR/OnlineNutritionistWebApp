using CoreLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        public Context(DbContextOptions<Context> options) : base(options) // Veri tabanı yolunu Program.cs dosyasına yönlendirdik.                                              
        {                                                               //  We redirected the database path to the program file.

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogFeature> BlogFeatures { get; set; }
        public DbSet<Books> Bookss { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Consultancy> Consultancys { get; set; }
        public DbSet<ContactUses> ContactUsess { get; set; }
        public DbSet<GetConsultancy> GetConsultancies { get; set; }


        //Configuration ile eklediğimiz tüm Assembly'lerimizi EfCore'a bildirdik.
        //We have notified EFCore about all our Assemblies that we have added with Configuration.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
