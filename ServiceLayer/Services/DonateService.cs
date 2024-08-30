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
    public class DonateService : IDonateService
    {
        private readonly IDonateRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DonateService(IDonateRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Donate donate)
        {
            await _repository.AddAsync(donate);
            await _unitOfWork.CommitAsync();
        }

        public bool DoesDonateExistForPackage(int id)
        {
            return _repository.DoesDonateExistForPackage(id);
        }

        public Task<IEnumerable<Donate>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<Donate> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}

