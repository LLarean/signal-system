using System;
using System.Collections.Generic;

namespace GameSignals
{
    /// <summary>
    /// Thread-safe signal system for global communication using interface-based subscribers.
    /// </summary>
    public static class SignalSystem
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<Type, SubscribersList<IGlobalSubscriber>> _subscribersByType = new();

        /// <summary>
        /// Subscribes an object to events based on the interfaces it implements.
        /// </summary>
        /// <param name="subscriber">The subscriber object.</param>
        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            var subscriberTypes = SignalSystemTypes.GetSubscriberTypes(subscriber);

            lock (_lock)
            {
                foreach (var type in subscriberTypes)
                {
                    _subscribersByType.TryAdd(type, new SubscribersList<IGlobalSubscriber>());
                    _subscribersByType[type].Add(subscriber);
                }
            }
        }

        /// <summary>
        /// Unsubscribes an object from all events.
        /// </summary>
        /// <param name="subscriber">The subscriber object.</param>
        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
            var subscriberTypes = SignalSystemTypes.GetSubscriberTypes(subscriber);

            lock (_lock)
            {
                foreach (var type in subscriberTypes)
                {
                    if (_subscribersByType.TryGetValue(type, out var subscriberList))
                        subscriberList.Remove(subscriber);
                }
            }
        }

        /// <summary>
        /// Raises an event for all subscribers of type <typeparamref name="TSubscriber"/>.
        /// </summary>
        /// <typeparam name="TSubscriber">The subscriber type.</typeparam>
        /// <param name="action">The action to perform for each subscriber.</param>
        /// <exception cref="Exception">Thrown if event handling fails.</exception>
        public static void Raise<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, IGlobalSubscriber
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            SubscribersList<IGlobalSubscriber> subscribers;

            lock (_lock)
            {
                _subscribersByType.TryGetValue(typeof(TSubscriber), out subscribers);
            }

            if (subscribers == null || subscribers.Subscribers.Count == 0) return;

            subscribers.SetExecuting(true);

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

            subscribers.SetExecuting(false);
            subscribers.Cleanup();
        }
    }
}