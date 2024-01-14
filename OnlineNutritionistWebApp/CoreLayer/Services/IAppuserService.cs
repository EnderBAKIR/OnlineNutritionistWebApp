using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IAppuserService: IService<AppUser>
    {
        public Task<List<AppUser>> GetNutritionWithService();
        public Task<List<AppUser>> GetNutritionists();
    }
}
