using AutoMapper;
using DataAccess.Entities;
using Service.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Users,ShowUserDto>();
            CreateMap<Users, ShowUserDto>().ReverseMap();
            CreateMap<BussinessEntities, ShowBussinessEntitiesDto>();
            CreateMap<BussinessEntities,AddBussinessEntitiesDto>().ReverseMap();    
           
        }
    }
}
