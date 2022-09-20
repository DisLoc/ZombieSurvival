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

    public void Add(ISubscriber subscriber)
    {
        if (_subscribers.Contains(subscriber)) return;
        
        _subscribers.Add(subscriber);
    }

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

    public void RaiseEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {
        foreach(ISubscriber subscriber in _subscribers)
        {
            action.Invoke((TSubscriber)subscriber);
        }
    }

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