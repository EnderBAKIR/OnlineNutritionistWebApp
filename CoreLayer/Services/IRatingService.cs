using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IRatingService
    {
        Task AddAsync(Rating rating);
        Task UpdateAsync(Rating rating);
        Task<Rating> GetByIdAsync(int id);
        Task<IEnumerable<Rating>> GetAllAsync();
    }
}
