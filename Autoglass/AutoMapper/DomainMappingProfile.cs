using Autoglass.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Autoglass.API.AutoMapper
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile() { 
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
