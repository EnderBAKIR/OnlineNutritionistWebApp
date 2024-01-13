using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IConsultancyService : IService<Consultancy>
    {
        public Task<List<Consultancy>> GetConsultancyForNutrition(int? id);
    }
}
