using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class GetBooksRepository : GenericRepository<GetBooks>, IGetBooksRepository
    {


        public GetBooksRepository(Context dbContext) : base(dbContext)
        {

        }

        public void ChangeToTrue(int id)
        {
            var values = _dbContext.GetBookss.Find(id);
            if (values != null)
            {
                values.status = true;
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<GetBooks>> RequestListForNutritionist(int id)
        {
            return await _dbContext.GetBookss.Include(x => x.Books).Include(x => x.AppUser).Where(x => x.Books.AppUserId == id).ToListAsync();
        }

        public async Task<List<GetBooks>> StatusListForUser(int id)
        {
            return await _dbContext.GetBookss.Include(x => x.Books).Include(x => x.AppUser).Where(x => x.AppUserId == id).ToListAsync();
        }
    }
}
