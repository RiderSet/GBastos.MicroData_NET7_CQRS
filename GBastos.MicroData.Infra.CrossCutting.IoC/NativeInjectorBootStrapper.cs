using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.Services;
using GBastos.MicroData.Domain.Core.Events;
using GBastos.MicroData.Domain.Interfaces;
using GBastos.MicroData.Infra.CrossCutting.Bus;
using GBastos.MicroData.Infra.Data.Context;
using GBastos.MicroData.Infra.Data.EventSourcing;
using GBastos.MicroData.Infra.Data.Repository;
using GBastos.MicroData.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using GBastos.MicroData.Domain.Commands.Item;
using GBastos.MicroData.Domain.Events.Item;

namespace GBastos.MicroData.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IItemAppService, ItemAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<ItemRegisteredEvent>, ItemEventHandler>();
            services.AddScoped<INotificationHandler<ItemUpdatedEvent>, ItemEventHandler>();
            services.AddScoped<INotificationHandler<ItemRemovedEvent>, ItemEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemCommand, ValidationResult>, ItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemCommand, ValidationResult>, ItemCommandHandler>();

            // Infra - Data
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<CTX>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}