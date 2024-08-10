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
    public class AssociationService : IAssociationService
    {
        private readonly IAssociationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AssociationService(IAssociationRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Association association)
        {
            await _repository.AddAsync(association);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Association>> GetAllAsync()
        {
            return  await _repository.GetAllAsync();
        }

        public async Task<Association> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task Remove(Association association)
        {
            _repository.Remove(association);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(Association association)
        {
            _repository.Update(association);
            await _unitOfWork.CommitAsync();
        }
    }
}
