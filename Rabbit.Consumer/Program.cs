using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Rabbit.Models;
using Rabbit.Models.Entities;

//Simple example for tests
var factory = new ConnectionFactory {
    HostName = "localhost",
    UserName = "admin",
    Password = "123456"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "rabbitMessagesQueue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var json = Encoding.UTF8.GetString(body);
    RabbitMessage message = JsonSerializer.Deserialize<RabbitMessage>(json);

    //System.Threading.Thread.Sleep(1000);

    Console.WriteLine($"Id: {message.ID}; Titulo: {message.Titulo}; Texto: {message.Texto};");
};
channel.BasicConsume(queue: "rabbitMessagesQueue",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();