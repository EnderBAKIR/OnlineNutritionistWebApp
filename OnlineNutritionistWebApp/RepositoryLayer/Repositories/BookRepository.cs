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
    public class BookRepository : GenericRepository<Books>, IBookRepository
    {
        public BookRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task <List<Books>> GetBooksWithNutrition(int id)
        {
            return await _dbContext.Bookss.Where(x => x.Id == id).Include(X => X.AppUser).ToListAsync();
        }

        public async Task<List<Books>> LastBooksAsync(int id)
        {
            return await _dbContext.Bookss.OrderByDescending(b => b.Id).Take(3).ToListAsync();
        }
    }
}
