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
    public class ConsultancyRepository : GenericRepository<Consultancy>, IConsultancyRepository
    {
        public ConsultancyRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<Consultancy>> GetConsultancyForNutrition(int? id)
        {
            return await _dbContext.Consultancys.Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }
    }
}
