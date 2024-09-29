using AutoMapper;
using DataAccess.dtos;
using DataAccess.Entities;
using Service.dtos;
using shared.dtos;
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
            CreateMap<BussinessEntities, ShowBusinessEntitiesDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
            CreateMap<BussinessEntities,AddBusinessEntitiesDto>().ReverseMap();
            CreateMap<UpdateBusinessEntitiesDto, BussinessEntities>();
            CreateMap<PaginationSortDto, PaginationSortDto_DataAccess>();

            CreateMap<AddCategoryDto, Categories>();
            CreateMap<Categories, ShowCategoryDto>();
            CreateMap<UpdateCategoryDto, Categories>();

        }
    }
}
