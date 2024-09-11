using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IDonateService
    {
        bool DoesDonateExistForPackage(int id);
        Task AddAsync(Donate donate);
        Task<Donate> GetByIdAsync(int id);
        Task<IEnumerable<Donate>> GetAllAsync();
        Task Update(Donate donate);
        Task Remove(Donate donate);
        Task<List<Donate>> GetDonateForNutritionistAsync(int id);
    }
}
