namespace DemoRabbitMq.Consumer
{
    public interface IMessageService
    {
        public void SetMessage(string message);
        public List<string> GetMessages();
    }
}
