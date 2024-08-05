using AutoMapper;
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
    public class BooksService : Service<Books>, IBooksService
    {
        private readonly IBookRepository _repository;


        public BooksService(IGenericRepository<Books> repository, IUnitOfWork unitOfWork, IBookRepository bookrepository) : base(repository, unitOfWork)
        {
            _repository = bookrepository;
        }

        public async Task<List<Books>> GetBookForNutrition(int id)
        {
            return await _repository.GetBookForNutrition(id);
        }


        public async Task<List<Books>> LastBooksAsync(int id)
        {
            return await _repository.LastBooksAsync(id);
        }

        public async Task<List<Books>> RequestListForNutritionist(int id)
        {
           return await _repository.RequestListForNutritionist(id);
        }

        async Task<List<Books>> IBooksService.GetBooksWithNutrition()
        {
            return await _repository.GetBooksWithNutrition();
        }
    }
}
