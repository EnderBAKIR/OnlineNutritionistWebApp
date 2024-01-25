using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IGetBooksService : IService<GetBooks>
    {
        public Task<List<GetBooks>> RequestListForNutritionist(int id);
        void ChangeToTrue(int id);
    }
}
