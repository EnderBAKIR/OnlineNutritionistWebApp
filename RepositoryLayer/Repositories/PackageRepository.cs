using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly Context _context;
        private readonly DbSet<Package> _DbSet;

        public PackageRepository(Context context)
        {
            _context = context;
            _DbSet = context.Set<Package>();
        }

        public async Task AddAsync(Package package)
        {
            await _DbSet.AddAsync(package);
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
           return await _DbSet.ToListAsync();
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return  await _DbSet.FindAsync(id);
        }

        public void Remove(Package package)
        {
             _DbSet.Remove(package);
        }

        public void Update(Package package)
        {
            _DbSet.Update(package);
        }
    }
}
