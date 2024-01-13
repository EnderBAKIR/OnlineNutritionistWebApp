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
    public class ConsultancyService : Service<Consultancy>, IConsultancyService
    {
        private readonly IConsultancyRepository _repository;
        public ConsultancyService(IGenericRepository<Consultancy> repository, IUnitOfWork unitOfWork , IConsultancyRepository consultancyRepository) : base(repository, unitOfWork)
        {
            _repository = consultancyRepository;
        }

        public async Task<List<Consultancy>> GetConsultancyForNutrition(int? id)
        {
            return await _repository.GetConsultancyForNutrition(id);
        }
    }
}
