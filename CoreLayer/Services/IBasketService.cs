using CoreLayer.Models;

namespace CoreLayer.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<Basket>> GetBasketByAppUserIdAsync(int appuserId);
        Task CreateBasketAsync(Basket basket);
        Task UpdateBasketAsync(Basket basket);
        Task RemoveBasketAsync(Basket basket);
    }
}
