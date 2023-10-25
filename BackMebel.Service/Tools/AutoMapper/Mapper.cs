using AutoMapper;
using BackMebel.Domain.Models.FeedBackModels;
using BackMebel.Domain.Models.ProductModels;
using BackMebel.Domain.Models.UserModels;
using BackMebel.Service.Dtos.FeedbackDtos;
using BackMebel.Service.Dtos.ProductDtos;
using BackMebel.Service.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Tools.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<User, UserDto>();
            
        }
    }

}
