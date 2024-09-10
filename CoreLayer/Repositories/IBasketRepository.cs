using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Basket>> GetBasketByAppUserIdAsync(int appuserId);
        Task CreateBasketAsync(Basket basket);
        void UpdateBasketAsync(Basket basket);
        void RemoveBasketAsync(Basket basket);
    }
}
