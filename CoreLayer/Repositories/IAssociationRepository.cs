using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IAssociationRepository
    {
        Task AddAsync(Association association);
        void Update(Association association);
        void Remove(Association association);
        Task<IEnumerable<Association>> GetAllAsync();
        Task<Association> GetByIdAsync(int id);
    }
}
