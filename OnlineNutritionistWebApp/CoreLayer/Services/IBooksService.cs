using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IBooksService: IService<Books>
    {
        public Task<List<Books>> LastBooksAsync(int id);
        public Task<List<Books>> GetBooksWithNutrition();
    }
}
