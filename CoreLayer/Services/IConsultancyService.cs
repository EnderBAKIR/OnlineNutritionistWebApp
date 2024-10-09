using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IConsultancyService
    {
        Task AddAsync(Consultancy consultancy);
        Task Update(Consultancy consultancy);
        Task RemoveAsync(Consultancy consultancy);
        Task<Consultancy> GetByIdAsync(int id);
        Task<List<Consultancy>> GetConsultancyForNutrition(int? id);
        Task<Consultancy> GetConsultancyAsync();
        Task<List<Consultancy>> GetConsultancyWithNutrition();
    }
}
