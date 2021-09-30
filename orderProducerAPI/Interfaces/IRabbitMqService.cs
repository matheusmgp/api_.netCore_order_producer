using orderProducerAPI.Entities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orderProducerAPI.Interfaces
{
    public interface IRabbitMqService
    {
        IModel GetChannel();

        void CreateConnection();

        bool SendToExchange(Order order, string exchange, string routingKey);
    }
}
