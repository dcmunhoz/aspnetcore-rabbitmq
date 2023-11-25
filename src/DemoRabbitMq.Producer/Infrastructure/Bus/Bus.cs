using RabbitMQ.Client;
using System.Text;

namespace DemoRabbitMq.Producer.Infrastructure.Bus
{
    public class Bus : IBus, IDisposable
    {
        private const string ExchangeName = "demo/fila-rabbitmq";
        private const string QueueName = "demo-fila-teste";
        private const string RoutingKey = "demo-key-teste";

        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public Bus()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection("Demo Asp.Net Core Producer");
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, RoutingKey, null); 
        }


        public Task PublicAsync(string message)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(ExchangeName, RoutingKey, false, null, bytes);

            return Task.CompletedTask;
        }


        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
