using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloudAmqpUrl = Environment.GetEnvironmentVariable("CLOUDAMQP_URL");

            var factory = new ConnectionFactory();
            if (cloudAmqpUrl == null)
            {
                factory.HostName = "localhost";
            }
            else
            {
                factory.Uri = new Uri(cloudAmqpUrl);
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                consumer.Shutdown += (model, e) =>
                {
                    Console.WriteLine("Shutdown " + e.ReplyText);
                };
                consumer.ConsumerCancelled += (sender, e) =>
                {
                    Console.WriteLine("Cancelled " + e.ConsumerTag);
                };      
                channel.BasicConsume(queue: "test",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}