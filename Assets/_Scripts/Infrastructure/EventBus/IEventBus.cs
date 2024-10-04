using System;
public interface IEventBus
{
    void Subscribe(string eventType, Action listener);
    void Unsubscribe(string eventType, Action listener);
    void Publish(string eventType);
}
