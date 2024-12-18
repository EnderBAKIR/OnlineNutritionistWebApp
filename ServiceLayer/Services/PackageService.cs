﻿using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWorks;

namespace ServiceLayer.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IPackageRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Package package)
        {
            await _repository.AddAsync(package);
            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Package> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Package>> GetLastPackageAsync(int id)
        {
            return await _repository.GetLastPackageAsync(id);
        }

        public async Task<List<Package>> GetPacgateForNutritionist(int id)
        {
            return await _repository.GetPacgateForNutritionist(id);
        }

        public async Task<List<Package>> GetPacgateWithNutritionist()
        {
            return await _repository.GetPacgateWithNutritionist();
        }

        public async Task<Package> GetPackageDetailAsync(int id)
        {
            return await _repository.GetPackageDetailAsync(id);
        }

        public async Task<Dictionary<int, int?>> GetPackageNutriIdsAsync()
        {
            return await _repository.GetPackageNutriIdsAsync();
        }

        public async Task Remove(Package package)
        {
             _repository.Remove(package);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(Package package)
        {
            _repository.Update(package);
            await _unitOfWork.CommitAsync();
        }
    }
}
