using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IRatingRepository
    {
        Task AddAsync(Rating rating);
        void UpdateAsync(Rating rating);
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task<Rating> GetRatingByUserAndNutriIdAsync(int userId, int nutriId);
    }
}
