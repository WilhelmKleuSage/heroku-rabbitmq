using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace HerokuRabbitMq.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return "OK";
        }

        [HttpPost]
        public void Post([FromBody]RequestMessage value)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = value.Message;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                    routingKey: "test",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

        }
    }

    public class RequestMessage
    {
        public string Message { get; set; }
    }
}