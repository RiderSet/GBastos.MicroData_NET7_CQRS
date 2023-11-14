using AutoMapper;
using GBastos.MicroData.Application.ViewModels;
using GBastos.MicroData.Domain.Commands.Item;

namespace GBastos.MicroData.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ItemViewModel, RegisterNewItemCommand>()
                .ConstructUsing(c => new RegisterNewItemCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<ItemViewModel, UpdateItemCommand>()
                .ConstructUsing(c => new UpdateItemCommand(c.Id, c.Name, c.Email, c.BirthDate));

            CreateMap<PedidoViewModel, RegisterNewPedidoCommand>()
                .ConstructUsing(c => new RegisterNewPedidoCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<PedidoViewModel, UpdatePedidoCommand>()
                .ConstructUsing(c => new UpdatePedidoCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
