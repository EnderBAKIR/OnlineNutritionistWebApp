﻿using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IPackageService
    {
        Task AddAsync(Package package);
        Task Update(Package package);
        Task Remove(Package package);
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package> GetByIdAsync(int id);
        Task<List<Package>> GetPacgateForNutritionist(int id);
        Task<List<Package>> GetPacgateWithNutritionist();
        Task<Package> GetPackageDetailAsync(int id);
        Task<List<Package>> GetLastPackageAsync(int id);
        Task<Dictionary<int, int?>> GetPackageNutriIdsAsync();
    }
}
