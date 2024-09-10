using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBasketAsync(Basket basket)
        {
            basket.CreatedDate = DateTime.Now;
            await _basketRepository.CreateBasketAsync(basket);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Basket>> GetBasketByAppUserIdAsync(int appuserId)
        {
            return await _basketRepository.GetBasketByAppUserIdAsync(appuserId);
        }

        public async Task RemoveBasketAsync(Basket basket)
        {
            _basketRepository.RemoveBasketAsync(basket);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBasketAsync(Basket basket)
        {
            _basketRepository.UpdateBasketAsync(basket);
            await _unitOfWork.CommitAsync();
        }
    }
}
