using AutoMapper;
using GBastos.MicroData.Application.ViewModels;
using GBastos.MicroData.Domain.Models;

namespace GBastos.MicroData.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Item, CustomerViewModel>();
        }
    }
}
