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
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IRatingRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Rating rating)
        {
            await _repository.AddAsync(rating);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<Rating> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Rating> GetRatingByUserAndNutriIdAsync(int userId, int nutriId)
        {
            return await _repository.GetRatingByUserAndNutriIdAsync(userId, nutriId);
        }

        public async Task UpdateAsync(Rating rating)
        {
            _repository.UpdateAsync(rating);
            await _unitOfWork.CommitAsync();
        }
    }
}
