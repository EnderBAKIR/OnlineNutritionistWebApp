using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly Context _context;
        private readonly DbSet<Rating> _Dbset;

        public RatingRepository(Context context)
        {
            _context = context;
            _Dbset = context.Set<Rating>();
        }

        public async Task AddAsync(Rating rating)
        {
            await _Dbset.AddAsync(rating);
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _Dbset.ToListAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _Dbset.FindAsync(id);
        }

        public void UpdateAsync(Rating rating)
        {
            _Dbset.Update(rating);
        }
    }
}
