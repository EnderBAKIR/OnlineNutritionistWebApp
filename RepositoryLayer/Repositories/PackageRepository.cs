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

        public async Task<List<Package>> GetLastPackageAsync(int id)
        {
            return await _context.Packages.OrderByDescending(p => p.Id).Take(2).ToListAsync();
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

        public async Task<Dictionary<int, int?>> GetPackageNutriIdsAsync()
        {
            //Paketleri açan diyetisyenlerin ID'sini çekme işlemi --- The process of capturing the IDs of the dietitians who opened the packages//

            // Paketleri açan diyetisyenlerin ID'lerini depolamak ve paketle eşleştirmek için anahtar-değer çiftleri kullanan bir sözlük oluşturuldu.
            // A dictionary was created that uses key-value pairs to store the IDs of dietitians who opened the packages and match them with the package.
            var packageNutriIds = new Dictionary<int, int?>();

            var packages = await _context.Packages
                .Include(x => x.AppUser) // AppUser'ı dahil et  //include AppUser
                .ToListAsync(); // Tüm paketleri al // Get all packages

            foreach (var package in packages)
            {
                // Her paketin ID'sini ve ilgili Nutri ID'sini sözlüğe ekle // Add the ID of each package and the corresponding Nutri ID that opened the package to the dictionary
                packageNutriIds[package.Id] = package.AppUser?.AppNutriId;
            }

            return packageNutriIds;
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
