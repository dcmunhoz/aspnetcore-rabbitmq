namespace DemoRabbitMq.Producer.Infrastructure.Bus
{
    public interface IBus
    {
        public Task PublicAsync(string message);
    }
}
