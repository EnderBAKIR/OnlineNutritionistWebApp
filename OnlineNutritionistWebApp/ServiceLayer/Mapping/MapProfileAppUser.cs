using AutoMapper;
using CoreLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class MapProfileAppUser :  Profile
    {
        public MapProfileAppUser() 
        {
            CreateMap<MapProfileAppUser , AppUserDto>().ReverseMap();
            
        }  
        
    }
}
