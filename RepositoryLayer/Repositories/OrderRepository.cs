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
    public class OrderRepository : IOrderRepository
    {
        private readonly Context _context;
        private readonly DbSet<Order> _orderSet;

        public OrderRepository(Context context)
        {
            _context = context;
            _orderSet = _context.Set<Order>();
        }
        public async Task CreateOrderAsync(Order Order)
        {
            await _orderSet.AddAsync(Order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByAppUserIdAsync(int appuserId)
        {
            return await _orderSet.Include(x => x.AppUser).Include(x=>x.OrderItems).Where(x => x.AppUserId == appuserId).ToListAsync();
        }

        public async Task<List<int>> GetPurchasedPackagesAsync(int appuserId)
        {
            return await _context.Orders.Where(x => x.AppUserId == appuserId).Select(x => x.Id) .ToListAsync();
        }

        public void RemoveOrderAsync(Order Order)
        {
            _orderSet.Remove(Order);
        }

        public void UpdateOrderAsync(Order Order)
        {
            _orderSet.Update(Order);
        }
    }
}
