using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IConsultancyRepository 
    {
        Task AddAsync(Consultancy consultancy);
        void Update(Consultancy consultancy);
        void RemoveAsync(Consultancy consultancy);
        Task<Consultancy> GetByIdAsync(int id);
        Task<List<Consultancy>> GetConsultancyForNutrition(int? id);
        Task<Consultancy> GetConsultancyAsync();
        Task<List<Consultancy>> GetConsultancyWithNutrition();
    }
}
