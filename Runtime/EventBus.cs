using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventBusSystem
{
    /// <summary>
    /// Provides a static event bus system for global subscriber communication.
    /// </summary>
    public static class EventBus
    {
        private static Dictionary<Type, SubscribersList<IGlobalSubscriber>> _subscribersByType = new();
        
        /// <summary>
        /// Subscribes an object to events based on its implemented interfaces.
        /// </summary>
        /// <param name="subscriber"></param>
        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            
            foreach (Type type in subscriberTypes)
            {
                _subscribersByType.TryAdd(type, new SubscribersList<IGlobalSubscriber>());
                _subscribersByType[type].Add(subscriber);
            }
        }
        
        /// <summary>
        /// Unsubscribes an object from all events.
        /// </summary>
        /// <param name="subscriber"></param>
        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = EventBusHelper.GetSubscriberTypes(subscriber);
            
            foreach (Type type in subscriberTypes)
            {
                if (_subscribersByType.TryGetValue(type, out var subscriberItem))
                    subscriberItem.Remove(subscriber);
            }
        }

        /// <summary>
        /// Raises an event for all subscribers of type <typeparamref name="TSubscriber"/>
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="TSubscriber"></typeparam>
        /// <exception cref="Exception"></exception>
        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            var subscribers = _subscribersByType.GetValueOrDefault(typeof(TSubscriber));
            
            if (subscribers == null) return;
            if (subscribers.Subscribers.Count == 0) return;
            
            subscribers.IsExecuting = true;

            foreach (IGlobalSubscriber subscriber in subscribers.Subscribers)
            {
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    throw new Exception($"Event failed for {subscriber.GetType()}", e);
                }
            }
            
            subscribers.IsExecuting = false;
            subscribers.Cleanup();
        }
    }
}