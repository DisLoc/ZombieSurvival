using System;
using System.Collections.Generic;

public class SubscriberList<TSubscriber> : CleanupableList<TSubscriber> where TSubscriber : class, ISubscriber
{
    public SubscriberList() : base() { }

    /// <summary>
    /// Add new subscriber
    /// </summary>
    /// <param name="subscriber">Subscriber need to add</param>
    public override void Add(TSubscriber item)
    {
        if (_list.Contains(item)) return;

        _list.Add(item);
    }


    /// <summary>
    /// Raising event for all subscribers
    /// </summary>
    /// <typeparam name="TSubscriber">Event interface</typeparam>
    /// <param name="action">Action lambda</param>
    public void RaiseEvent<TSub>(Action<TSub> action) where TSub : ISubscriber
    {
        foreach(ISubscriber subscriber in _list)
        {
            action.Invoke((TSub)subscriber);
        }
    }
}