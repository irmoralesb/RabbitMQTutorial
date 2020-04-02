using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "172.17.0.2"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        autoDelete: false,
                        exclusive: false,
                        arguments: null);
                    
                    string message = "Hello World";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);

                    Console.Out.WriteLine(" [x] Sent {0}", message);
                }
                Console.WriteLine("Press [enter] to exit.");
                Console.In.ReadLine();
            }
        }
    }
}
