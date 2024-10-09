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
    public class ConsultancyRepository : IConsultancyRepository
    {
        private readonly Context _context;
        private readonly DbSet<Consultancy> _DbSet;

        public ConsultancyRepository(Context context)
        {
            _context = context;
            _DbSet = context.Set<Consultancy>();
        }

        public async Task AddAsync(Consultancy consultancy)
        {
            await _DbSet.AddAsync(consultancy);
        }

        public async Task<Consultancy> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<Consultancy> GetConsultancyAsync()
        {
            return await _context.Consultancys.Include(X => X.AppUser).FirstOrDefaultAsync();
        }

        public async Task<List<Consultancy>> GetConsultancyForNutrition(int? id)
        {
            return await _context.Consultancys.Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }

        public async Task<List<Consultancy>> GetConsultancyWithNutrition()
        {
            return await _context.Consultancys.Include(X => X.AppUser).ToListAsync();
        }

        public void RemoveAsync(Consultancy consultancy)
        {
            _DbSet.Remove(consultancy);
        }

        public void Update(Consultancy consultancy)
        {
            _DbSet.Update(consultancy);
        }
    }
}
