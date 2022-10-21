using System;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor.Experimental.GraphView;

public static class EventBus
{
    static Dictionary<Type, SubscriberList> _subscribers = new Dictionary<Type, SubscriberList>();

    static bool _onPublish;

    /// <summary>
    /// Subscribe on some events
    /// </summary>
    /// <param name="subscriber">Subscriber need to subscribe</param>
    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType()); // Find events need to subscribe

        foreach(Type interfaceType in interfaces)
        {
            if (!_subscribers.ContainsKey(interfaceType)) // if that event hasnt subscribers create new list
            {
                _subscribers[interfaceType] = new SubscriberList();
            }

            _subscribers[interfaceType].Add(subscriber);
        }
    }

    /// <summary>
    /// Unsubscribe from all events
    /// </summary>
    /// <param name="subscriber">Subscriber need to unsubscribe</param>
    public static void Unsubscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType()); // Find events need to unsubscribe

        foreach (Type interfaceType in interfaces)
        {
            if (!_subscribers.ContainsKey(interfaceType)) // if that event hasnt subscribers (???)
            {
                continue;
            }

            _subscribers[interfaceType].Remove(subscriber, _onPublish); // Remove subscriber
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="subscriber">Subscriber that need to get interfaces</param>
    /// <returns>Return all interfaces that inherited from ISubscriber</returns>
    private static List<Type> GetSubscriberInterfaces(Type subscriber)
    {
        List<Type> interfaces = new List<Type>();

        foreach(Type type in subscriber.GetInterfaces())
        {
            if (typeof(ISubscriber).IsAssignableFrom(type) && type != typeof(ISubscriber))
                interfaces.Add(type);
        }

        return interfaces;
    }

    /// <summary>
    /// Raise event
    /// </summary>
    /// <typeparam name="TSubscriber">Event interface</typeparam>
    /// <param name="action">Action lambda</param>
    public static void Publish<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {
        Type type = typeof(TSubscriber);

        if (!_subscribers.ContainsKey(type))
        {
            return;
        }

        _onPublish = true; // from this moment subscribers will be set null value instead of removing

        _subscribers[type].RaiseEvent(action);
        _subscribers[type].Cleanup();

        _onPublish = false; // from this moment subscribers will just removing
    }
}