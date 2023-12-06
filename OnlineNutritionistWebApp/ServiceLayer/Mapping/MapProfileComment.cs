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
    public class MapProfileComment : Profile
    {
        public MapProfileComment()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
