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
    public class ConsultancyService : IConsultancyService
    {
        private readonly IConsultancyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConsultancyService(IConsultancyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Consultancy consultancy)
        {
            await _repository.AddAsync(consultancy);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Consultancy> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Consultancy> GetConsultancyAsync()
        {
            return await _repository.GetConsultancyAsync();
        }

        public async Task<List<Consultancy>> GetConsultancyForNutrition(int? id)
        {
            return await _repository.GetConsultancyForNutrition(id);
        }

        public async Task<List<Consultancy>> GetConsultancyWithNutrition()
        {
            return await _repository.GetConsultancyWithNutrition();
        }

        public async Task RemoveAsync(Consultancy consultancy)
        {
            _repository.RemoveAsync(consultancy);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(Consultancy consultancy)
        {
            _repository.Update(consultancy);
            await _unitOfWork.CommitAsync();
        }
    }
}
