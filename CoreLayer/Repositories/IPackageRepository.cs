using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IPackageRepository
    {
        Task AddAsync(Package package);
        void Update(Package package);
        void Remove(Package package);
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<List<Package>> GetPacgateForNutritionist(int id);
        Task<List<Package>> GetPacgateWithNutritionist();
        Task<Package> GetPackageDetailAsync(int id);
        Task<List<Package>> GetLastPackageAsync(int id);
    }
}
