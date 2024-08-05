using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IGetBooksRepository : IGenericRepository<GetBooks>
    {
        public Task<List<GetBooks>> RequestListForNutritionist(int id);
        public Task<List<GetBooks>> StatusListForUser(int id);
        void ChangeToTrue(int id);
    }
}
