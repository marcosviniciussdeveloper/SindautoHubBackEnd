public interface IMessageBus
{
    void Publish(string queueName, string message);
    void Subscribe(string queueName, Action<string> onMessage);
}
