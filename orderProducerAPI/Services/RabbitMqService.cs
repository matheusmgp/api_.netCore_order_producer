using orderProducerAPI.Entities;
using orderProducerAPI.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace orderProducerAPI.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        public IModel _channel;
        public IConnection _connection;
        ConnectionFactory _factory;

        public RabbitMqService()
        {
            CreateConnection();
        }
        public IModel GetChannel()
        {
            return _channel;
        }

        public void CreateConnection()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            _factory.UserName = "admin";
            _factory.Password = "101010";
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public bool SendToExchange(Order order, string exchange, string routingKey)
        {
            string message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: exchange,
                                  routingKey: routingKey,
                                  basicProperties: null,
                                  body: body);

            return true;
        }
        /*public void CreateConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var _factory = new ConnectionFactory();

            config.GetSection("RabbitMqConnection").Bind(_factory);
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }*/


    }
}
