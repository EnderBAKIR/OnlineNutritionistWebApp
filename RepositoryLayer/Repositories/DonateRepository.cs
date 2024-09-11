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
    public class DonateRepository : IDonateRepository
    {

        private readonly Context _context;
        private readonly DbSet<Donate> _DbSet;

        public DonateRepository(Context context)
        {
            _context = context;
            _DbSet = context.Set<Donate>();
        }

        public async Task AddAsync(Donate donate)
        {
              _DbSet.Add(donate);
        }

        public bool DoesDonateExistForPackage(int id)
        {
            return _context.Donates.Any(d => d.AppUserId == id);
        }

        public async Task<IEnumerable<Donate>> GetAllAsync()
        {
            return await _context.Donates.Include(x => x.AppUser).ToListAsync();
        }

        public async Task<Donate> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<List<Donate>> GetDonateForNutritionistAsync(int id)
        {
            return await _context.Donates.Include(x=> x.AppUser).Where(x=> x.AppUserId == id).ToListAsync();
        }

        public void Remove(Donate donate)
        {
            _DbSet.Remove(donate);
        }

        public void Update(Donate donate)
        {
           _DbSet.Update(donate);
        }
    }
}
