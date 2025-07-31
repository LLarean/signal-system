using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GameSignals
{
    /// <summary>
    /// Provides helper methods for managing event subscribers in the EventBus system.
    /// </summary>
    internal static class EventBusTypes
    {
        private static readonly ConcurrentDictionary<Type, List<Type>> _cachedSubscriberTypes = new();

        /// <summary>
        /// Retrieves all subscriber interfaces implemented by the given global subscriber.
        /// </summary>
        /// <param name="globalSubscriber">The global subscriber instance.</param>
        /// <returns>List of subscriber interface types.</returns>
        /// <exception cref="ArgumentNullException">Thrown if globalSubscriber is null.</exception>
        public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            if (globalSubscriber == null)
                throw new ArgumentNullException(nameof(globalSubscriber));

            Type type = globalSubscriber.GetType();

            return _cachedSubscriberTypes.GetOrAdd(type, t =>
                t.GetInterfaces()
                    .Where(i => typeof(IGlobalSubscriber).IsAssignableFrom(i))
                    .ToList());
        }
    }
}