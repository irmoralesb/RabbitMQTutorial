﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
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
                    channel.QueueDeclare(
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.Out.WriteLine(" [x] Received {0}", message);
                    };

                    channel.BasicConsume(queue:"hello",
                        autoAck: true,
                        consumer: consumer);

                    Console.Out.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }


    }
}
