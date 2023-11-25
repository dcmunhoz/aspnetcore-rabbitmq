using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DemoRabbitMq.Consumer
{
    public class QueueWorker : BackgroundService
    {
        private readonly IMessageService _messageService;

        public QueueWorker(IMessageService messageService)
        {
            _messageService = messageService;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory connectionFactory = new() { HostName = "localhost" };
            IConnection connection = connectionFactory.CreateConnection("demo-rabbitmq-aspnetcore");
            IModel channel = connection.CreateModel();

            string exchangeName = "demo/fila-rabbitmq";
            string queueName = "demo-fila-teste";
            string routingKey = "demo-key-teste";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                var messageBody = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(messageBody);

                _messageService.SetMessage(message);

                channel.BasicAck(args.DeliveryTag, false);


                Console.WriteLine($"Mensagem: {message}");
            };

            channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }
    }
}
