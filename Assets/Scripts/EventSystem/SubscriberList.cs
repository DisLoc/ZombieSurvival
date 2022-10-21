using System;
using System.Collections.Generic;

public class SubscriberList
{
    List<ISubscriber> _subscribers;

    bool _needCleanup;

    public SubscriberList()
    {
        _subscribers = new List<ISubscriber>();
        _needCleanup = false;
    }

    /// <summary>
    /// Add new subscriber
    /// </summary>
    /// <param name="subscriber">Subscriber need to add</param>
    public void Add(ISubscriber subscriber)
    {
        if (_subscribers.Contains(subscriber)) return;
        
        _subscribers.Add(subscriber);
    }

    /// <summary>
    /// Remove subscriber from list. If onPublish equals true, subscriber will be set null value
    /// </summary>
    /// <param name="subscriber">Subscriber need to remove</param>
    /// <param name="onPublish">Is there raise event in progress?</param>
    /// <returns>Return true if remove subscriber or this sub setted null</returns>
    public bool Remove(ISubscriber subscriber, bool onPublish)
    {
        if (_subscribers.Contains(subscriber))
        {
            if (onPublish)
            {
                _needCleanup = true;
                _subscribers[_subscribers.IndexOf(subscriber)] = null;
                return true;
            }
            else
            {
                return _subscribers.Remove(subscriber);
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Raising event for all subscribers
    /// </summary>
    /// <typeparam name="TSubscriber">Event interface</typeparam>
    /// <param name="action">Action lambda</param>
    public void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {
        foreach(ISubscriber subscriber in _subscribers)
        {
            action.Invoke((TSubscriber)subscriber);
        }
    }

    /// <summary>
    /// Remove all null subscribers
    /// </summary>
    public void Cleanup()
    {
        if (_needCleanup)
        {
            _subscribers.RemoveAll(sub => sub == null);
            _needCleanup = false;
        }

        else return;
    }
}