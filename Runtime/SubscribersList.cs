using System;
using System.Collections.Generic;

namespace GameSignals
{
    /// <summary>
    /// Thread-unsafe list of event subscribers with deferred removal support during iteration.
    /// </summary>
    /// <typeparam name="TSubscriber">Type of subscriber that must be a reference type.</typeparam>
    public class SubscribersList<TSubscriber> where TSubscriber : class
    {
        private readonly List<TSubscriber> _subscribers = new();
        private bool _needsCleanUp;
        private bool _isExecuting;
        private readonly object _lock = new();

        /// <summary>
        /// Indicates whether the subscribers list is currently being iterated.
        /// </summary>
        public bool IsExecuting
        {
            get { lock (_lock) { return _isExecuting; } }
            private set { lock (_lock) { _isExecuting = value; } }
        }

        /// <summary>
        /// Gets a read-only view of current subscribers.
        /// </summary>
        public IReadOnlyList<TSubscriber> Subscribers
        {
            get { lock (_lock) { return _subscribers.AsReadOnly(); } }
        }

        /// <summary>
        /// Adds a new subscriber to the list.
        /// </summary>
        /// <param name="subscriber">Subscriber to add. Must not be null.</param>
        public void Add(TSubscriber subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));
            lock (_lock)
            {
                _subscribers.Add(subscriber);
            }
        }

        /// <summary>
        /// Removes a subscriber from the list.
        /// </summary>
        /// <param name="subscriber">Subscriber to remove.</param>
        public void Remove(TSubscriber subscriber)
        {
            lock (_lock)
            {
                if (!_isExecuting)
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
        }

        /// <summary>
        /// Cleans up null references from the subscribers list.
        /// </summary>
        public void Cleanup()
        {
            lock (_lock)
            {
                if (!_needsCleanUp) return;
                _subscribers.RemoveAll(subscriber => subscriber == null);
                _needsCleanUp = false;
            }
        }

        /// <summary>
        /// Sets the execution state for iteration.
        /// </summary>
        /// <param name="executing">True if iteration is in progress.</param>
        public void SetExecuting(bool executing)
        {
            IsExecuting = executing;
        }
    }
}