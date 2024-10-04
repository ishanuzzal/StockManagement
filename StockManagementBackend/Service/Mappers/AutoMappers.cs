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
            CreateMap<Categories, ShowCategoryDropdown>();
            CreateMap<UpdateCategoryDto, Categories>();
            CreateMap<AddProductDto, Product>();
            CreateMap<Product, ShowProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.Name));

            CreateMap<AddProductDto, BuyTransactionDto>()
                .ForMember(dest => dest.BussinessEntitiesId, opt => opt.MapFrom(src => src.BusinessEntitiesId))
                .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.StockAmount))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.StockAmount));
            CreateMap<Transactions, ShowTransactionDto>()
                .ForMember(dest => dest.TransactionTypes, opt => opt.MapFrom(src => src.TransactionTypes.ToString()));

            CreateMap<Product, SellTransactionDto>();

            CreateMap<BuyTransactionDto, Transactions>();
            CreateMap<SellProductDto, SellTransactionDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Qty))
                .ForMember(dest => dest.ProductsId, opt=>opt.MapFrom(src => src.Id));
            CreateMap<SellTransactionDto, Transactions>();

                
                
        }
    }
}
