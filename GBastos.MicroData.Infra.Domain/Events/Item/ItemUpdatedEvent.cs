﻿using System;
using NetDevPack.Messaging;

namespace GBastos.MicroData.Domain.Events.Item
{
    public class ItemUpdatedEvent : Event
    {
        public ItemUpdatedEvent(Guid id, string name, string email, DateTime birthDate)
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