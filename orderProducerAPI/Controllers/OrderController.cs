using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using orderProducerAPI.Entities;
using orderProducerAPI.Interfaces;
using System;

namespace orderProducerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;
        private IRabbitMqService _IRabbitMqService;


        public OrderController(ILogger<OrderController> logger, IRabbitMqService iRabbitMqService)
        {
            _logger = logger;
            _IRabbitMqService = iRabbitMqService;
        }


        [HttpPost("sendOrderToQueue")]
        public IActionResult SendOrderToQueue(Order order)
        {
            try

            {
                _IRabbitMqService.SendToExchange(order, "pedidos", "pedidos_key");
                
                return Accepted(order);

            }
            catch (Exception e)
            {
                _logger.LogError("Erro ao tentar criar uma nova Order", e);

                return new StatusCodeResult(500);
            }

        }
    }
}