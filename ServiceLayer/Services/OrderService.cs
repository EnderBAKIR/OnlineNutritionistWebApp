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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrderAsync(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByAppUserIdAsync(int appuserId)
        {
            return await _orderRepository.GetOrdersByAppUserIdAsync(appuserId);
        }

        public async Task RemoveOrderAsync(Order order)
        {
            _orderRepository.RemoveOrderAsync(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _orderRepository.UpdateOrderAsync(order);
            await _unitOfWork.CommitAsync();
        }
    }
}
