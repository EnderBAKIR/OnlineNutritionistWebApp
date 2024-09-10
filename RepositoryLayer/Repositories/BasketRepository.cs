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
    public class BasketRepository : IBasketRepository
    {
        private readonly Context _context;
        private readonly DbSet<Basket> _basketSet;

        public BasketRepository(Context context)
        {
            _context = context;
            _basketSet = _context.Set<Basket>();
        }

        public async Task CreateBasketAsync(Basket basket)
        {
            await _basketSet.AddAsync(basket);
        }

        public async Task<IEnumerable<Basket>> GetBasketByAppUserIdAsync(int appuserId)
        {
            return await _basketSet.Include(x=>x.AppUser).Where(x=>x.AppUserId == appuserId).ToListAsync();
        }

        public  void RemoveBasketAsync(Basket basket)
        {
           _basketSet.Remove(basket);
        }

        public void UpdateBasketAsync(Basket basket)
        {
            _basketSet.Update(basket);
        }
    }
}
