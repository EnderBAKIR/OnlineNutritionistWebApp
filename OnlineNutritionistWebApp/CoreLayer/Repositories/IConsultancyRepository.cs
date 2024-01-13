using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IConsultancyRepository : IGenericRepository<Consultancy>
    {
        public Task<List<Consultancy>> GetConsultancyForNutrition(int? id);
    }
}
