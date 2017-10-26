using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace Worker
{
    internal class Program
    {
        private static void Main(string[] args)
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
                factory.RequestedHeartbeat = 30;
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("test",
                    false,
                    false,
                    false,
                    null);

                using (var subscription = new Subscription(channel, "test", false))
                {
                    while (true)
                    {
                        if (!channel.IsOpen)
                        {
                            Console.WriteLine(
                                "The channel is no longer open, but we are still trying to process messages.");
                            throw new InvalidOperationException("Channel is closed.");
                        }
                        if (!connection.IsOpen)
                        {
                            Console.WriteLine(
                                "The connection is no longer open, but we are still trying to process message.");
                            throw new InvalidOperationException("Connection is closed.");
                        }

                        var gotMessage = subscription.Next(250, out var result);

                        if (gotMessage)
                        {
                            Console.WriteLine("Received message");
                            try
                            {
                                var body = result.Body;
                                var message = Encoding.UTF8.GetString(body);
                                Console.WriteLine(" [x] Received {0}", message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception caught while processing message. Will be bubbled up.", e);
                                throw;
                            }

                            Console.WriteLine("Acknowledging message completion");
                            subscription.Ack(result);
                        }
                    }
                }
            }
        }
    }
}