using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IAssociationService
    {
        Task AddAsync(Association association);
        Task Update(Association association);
        Task Remove(Association association);
        Task<IEnumerable<Association>> GetAllAsync();
        Task<Association> GetByIdAsync(int id);
    }
}
