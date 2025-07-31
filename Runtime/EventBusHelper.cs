using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBusSystem
{
    /// <summary>
    /// Provides helper methods for managing event subscribers in the EventBus system
    /// </summary>
    internal static class EventBusHelper
    {
        private static Dictionary<Type, List<Type>> _cachedSubscriberTypes = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// Retrieves all subscriber interfaces implemented by the given global subscriber.
        /// </summary>
        public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            Type type = globalSubscriber.GetType();
            
            if (_cachedSubscriberTypes.TryGetValue(type, out var types))
            {
                return types;
            }

            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(t => typeof(IGlobalSubscriber).IsAssignableFrom(t))
                .ToList();

            _cachedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}