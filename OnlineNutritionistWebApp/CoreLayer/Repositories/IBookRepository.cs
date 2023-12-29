using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IBookRepository: IGenericRepository<Books>
    {
        public Task <List<Books>> LastBooksAsync  (int id);
        public Task <List<Books>> GetBooksWithNutrition(int id);
    }
}
