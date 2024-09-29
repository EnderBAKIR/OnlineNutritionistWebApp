using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByAppUserIdAsync(int appuserId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task RemoveOrderAsync(Order order);
        Task<List<int>> GetPurchasedPackagesAsync(int appuserId);
    }
}
