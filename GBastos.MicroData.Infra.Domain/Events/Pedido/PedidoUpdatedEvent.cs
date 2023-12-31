﻿using System;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Events
{
    public class PedidoUpdatedEvent : Event
    {
        public PedidoUpdatedEvent(Guid id, string name, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }
    }
}