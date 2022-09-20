using System;
using System.Collections.Generic;

public static class EventBus
{
    static Dictionary<Type, SubscriberList> _subscribers = new Dictionary<Type, SubscriberList>();

    static bool _onPublish;

    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType());

        foreach(Type interfaceType in interfaces)
        {
            if (!_subscribers.ContainsKey(interfaceType))
            {
                _subscribers[interfaceType] = new SubscriberList();
            }

            _subscribers[interfaceType].Add(subscriber);
        }
    }

    public static void Unsubscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType());

        foreach (Type interfaceType in interfaces)
        {
            if (!_subscribers.ContainsKey(interfaceType))
            {
                return;
            }

            _subscribers[interfaceType].Remove(subscriber, _onPublish);
        }
    }

    static List<Type> GetSubscriberInterfaces(Type subscriber)
    {
        List<Type> interfaces = new List<Type>();

        foreach(Type type in subscriber.GetInterfaces())
        {
            if (typeof(ISubscriber).IsAssignableFrom(type) && type != typeof(ISubscriber))
                interfaces.Add(type);
        }

        return interfaces;
    }

    public static void Publish<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {
        Type type = typeof(TSubscriber);

        if (!_subscribers.ContainsKey(type))
        {
            return;
        }

        _onPublish = true;

        _subscribers[type].RaiseEvent(action);
        _subscribers[type].Cleanup();

        _onPublish = false;
    }
}