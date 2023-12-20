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
    public class AppUserService : Service<AppUser>, IAppUserService
    {
        private readonly IAppUserRepository _appuserRepository;
        private readonly IMapper _mapper;

        public AppUserService(IGenericRepository<AppUser> repository, IUnitOfWork unitOfWork , IAppUserRepository appUserRepository , IMapper mapper) : base(repository, unitOfWork)
        {
            _appuserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<List<AppUser>> GetAll()
        {
            var appuser = _appuserRepository.GetAll();

            var user = _mapper.Map<List<AppUser>>(appuser);
            return  user.ToList();
        }
    }
}
