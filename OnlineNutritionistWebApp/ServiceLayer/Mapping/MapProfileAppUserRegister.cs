using AutoMapper;
using CoreLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class MapProfileAppUserRegister : Profile
    {
        public MapProfileAppUserRegister() 
        {
            CreateMap<MapProfileAppUserRegister, AppUserRegisterDto>().ReverseMap();
        }
    }
}
