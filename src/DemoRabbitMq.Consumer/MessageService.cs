namespace DemoRabbitMq.Consumer
{
    public class MessageService : IMessageService
    {
        private IList<string> _messages;


        public MessageService()
        {
            _messages = new List<string>();
        }

        public List<string> GetMessages()
        {
            return _messages.ToList();
        }

        public void SetMessage(string message)
        {
            _messages.Add(message);
        }
    }
}
