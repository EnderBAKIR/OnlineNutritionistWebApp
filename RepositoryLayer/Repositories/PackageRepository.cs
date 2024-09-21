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
            return await _DbSet.FindAsync(id);
        }

        public async Task<List<Package>> GetPacgateForNutritionist(int id)
        {
            return await _context.Packages.Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<List<Package>> GetPacgateWithNutritionist()
        {
            return await _context.Packages.Include(X => X.AppUser).ToListAsync();
        }

        public async Task<Package> GetPackageDetailAsync(int id)
        {
            return await _context.Packages.Include(x => x.AppUser).Where(x => x.Id == id).FirstOrDefaultAsync();
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
