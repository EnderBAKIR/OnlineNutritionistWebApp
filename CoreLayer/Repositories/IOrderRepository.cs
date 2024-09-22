using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByAppUserIdAsync(int appuserId);
        Task CreateOrderAsync(Order order);
        void UpdateOrderAsync(Order order);
        void RemoveOrderAsync(Order order);
    }
}
