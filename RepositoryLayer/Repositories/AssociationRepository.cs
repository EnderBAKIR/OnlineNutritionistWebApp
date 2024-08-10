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
    public class AssociationRepository : IAssociationRepository
    {
        private readonly Context _context;
        private readonly DbSet<Association> _DbSet;

        public AssociationRepository(Context context)
        {
            _context = context;
            _DbSet = context.Set<Association>();
        }

        public async Task AddAsync(Association association)
        {
            await _DbSet.AddAsync(association);
        }

        public async Task<IEnumerable<Association>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();

        }

        public void Remove(Association association)
        {
            _DbSet.Remove(association);
        }

        public void Update(Association association)
        {
            _DbSet.Update(association);
        }
    }
}
