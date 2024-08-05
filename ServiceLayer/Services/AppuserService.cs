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
    internal class AppuserService : Service<AppUser>, IAppuserService
    {
        private readonly IAppuserRepository _appuserRepository;
        public AppuserService(IGenericRepository<AppUser> repository, IUnitOfWork unitOfWork, IAppuserRepository appuserRepository) : base(repository, unitOfWork)
        {
            _appuserRepository = appuserRepository;
        }

        public async Task<AppUser> GetNutritionistById(int id)
        {
            return await _appuserRepository.GetNutritionistById(id);
        }

        public async Task<List<AppUser>> GetNutritionists()
        {
            return await _appuserRepository.GetNutritionists();
        }

        public async Task<List<AppUser>> GetNutritionWithService()
        {
           return await _appuserRepository.GetNutritionWithService();
        }
    }
}
