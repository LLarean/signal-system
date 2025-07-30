using System;
using System.Collections.Generic;

namespace EventBusSystem
{
    /// <summary>
    /// Thread-unsafe list of event subscribers with deferred removal support during iteration.
    /// </summary>
    /// <typeparam name="TSubscriber">Type of subscriber that must be a reference type.</typeparam>
    /// <remarks>
    /// This implementation allows safe subscriber removal during event iteration
    /// by marking slots as null and performing cleanup afterwards.
    /// </remarks>
    internal class SubscribersList<TSubscriber> where TSubscriber : class
    {
        /// <summary>
        /// Indicates whether the subscribers list is currently being iterated.
        /// </summary>
        /// <value>True if iteration is in progress; otherwise false.</value>
        public bool IsExecuting;

        private readonly List<TSubscriber> _subscribers = new List<TSubscriber>();
        private bool _needsCleanUp = false;
        
        /// <summary>
        /// Gets a read-only view of current subscribers.
        /// </summary>
        /// <remarks>
        /// During iteration, this collection may contain null entries
        /// that will be removed by <see cref="Cleanup"/>.
        /// </remarks>
        public IReadOnlyList<TSubscriber> Subscribers => _subscribers;

        /// <summary>
        /// Adds a new subscriber to the list.
        /// </summary>
        /// <param name="subscriber">Subscriber to add. Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if subscriber is null.</exception>
        public void Add(TSubscriber subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));
                
            _subscribers.Add(subscriber);
        }

        /// <summary>
        /// Removes a subscriber from the list.
        /// </summary>
        /// <param name="subscriber">Subscriber to remove.</param>
        /// <remarks>
        /// If called during iteration (<see cref="IsExecuting"/> == true),
        /// marks the subscriber as null for deferred removal.
        /// </remarks>
        public void Remove(TSubscriber subscriber)
        {
            if (IsExecuting == false)
            {
                _subscribers.Remove(subscriber);
            }
            else
            {
                var index = _subscribers.IndexOf(subscriber);
                if (index >= 0)
                {
                    _needsCleanUp = true;
                    _subscribers[index] = null;
                }
            }
        }

        /// <summary>
        /// Cleans up null references from the subscribers list.
        /// </summary>
        /// <remarks>
        /// Should be called after iteration completes to remove
        /// subscribers that were marked as null during removal.
        /// </remarks>
        public void Cleanup()
        {
            if (_needsCleanUp == false) return;

            _subscribers.RemoveAll(subscriber => subscriber == null);
            _needsCleanUp = false;
        }
    }
}