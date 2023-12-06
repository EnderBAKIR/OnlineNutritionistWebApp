using AutoMapper;
using CoreLayer.DTOs;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class MapProfileBooks : Profile
    {
        public MapProfileBooks()
        {

            CreateMap<Books, BooksDto>().ReverseMap();
        }
    }
}
