using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class GetBooksService : Service<GetBooks>, IGetBooksService
    {
        private readonly IGetBooksRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public GetBooksService(IGenericRepository<GetBooks> repository, IUnitOfWork unitOfWork , IGetBooksRepository getBooksRepository) : base(repository, unitOfWork)
        {
            _repository = getBooksRepository;
            _unitOfWork = unitOfWork;
        }

        public void ChangeToTrue(int id)
        {
          _repository.ChangeToTrue(id);
             
        }

        public async Task<List<GetBooks>> RequestListForNutritionist(int id)
        {
           return await _repository.RequestListForNutritionist(id);
        }

        public async Task<List<GetBooks>> StatusListForUser(int id)
        {
            return await _repository.StatusListForUser(id);
        }
    }
}
