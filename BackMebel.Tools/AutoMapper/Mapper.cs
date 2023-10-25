using AutoMapper;
using BackMebel.Domain.Models.ProductModels;
using BackMebel.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Tools.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDto>
            
        }
    }
}
