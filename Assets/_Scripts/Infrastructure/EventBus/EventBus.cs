using System;
using System.Collections.Generic;

public class EventBus : IEventBus
{
    private readonly Dictionary<string, Action> _eventDictionary = new Dictionary<string, Action>();

    public void Subscribe(string eventType, Action listener)
    {
        if (!_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType] = listener;
        }
        else
        {
            _eventDictionary[eventType] += listener;
        }
    }

    public void Unsubscribe(string eventType, Action listener)
    {
        if(_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType] -= listener;
        }
    }

    public void Publish(string eventType)
    {
        if (_eventDictionary.ContainsKey(eventType))
        {
            _eventDictionary[eventType]?.Invoke();
        }
    }
}
