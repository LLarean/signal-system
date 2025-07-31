using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameSignals.Tests.Runtime
{
    public interface ITestEvent : IGlobalSubscriber
    {
        void OnEvent();
    }

    public class TestSubscriber : ITestEvent
    {
        public int CallCount { get; private set; }
        public void OnEvent() => CallCount++;
    }

    [TestFixture]
    public class SignalSystemTests
    {
        [SetUp]
        public void SetUp()
        {
            var field = typeof(SignalSystem).GetField("_subscribersByType", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var dict = (Dictionary<Type, SubscribersList<IGlobalSubscriber>>)field.GetValue(null);
            dict.Clear();
        }

        [Test]
        public void Subscribe_AddsSubscriber()
        {
            var subscriber = new TestSubscriber();
            SignalSystem.Subscribe(subscriber);

            SignalSystem.Raise<ITestEvent>(s => s.OnEvent());
            Assert.AreEqual(1, subscriber.CallCount);
        }

        [Test]
        public void Unsubscribe_RemovesSubscriber()
        {
            var subscriber = new TestSubscriber();
            SignalSystem.Subscribe(subscriber);
            SignalSystem.Unsubscribe(subscriber);

            SignalSystem.Raise<ITestEvent>(s => s.OnEvent());
            Assert.AreEqual(0, subscriber.CallCount);
        }

        [Test]
        public void Raise_MultipleSubscribers_AllAreCalled()
        {
            var s1 = new TestSubscriber();
            var s2 = new TestSubscriber();
            SignalSystem.Subscribe(s1);
            SignalSystem.Subscribe(s2);

            SignalSystem.Raise<ITestEvent>(s => s.OnEvent());
            Assert.AreEqual(1, s1.CallCount);
            Assert.AreEqual(1, s2.CallCount);
        }

        [Test]
        public void Subscribe_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SignalSystem.Subscribe(null));
        }

        [Test]
        public void Unsubscribe_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SignalSystem.Unsubscribe(null));
        }

        [Test]
        public void Raise_NullAction_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SignalSystem.Raise<ITestEvent>(null));
        }

        [Test]
        public void Raise_ExceptionInSubscriber_ThrowsAndWraps()
        {
            var badSubscriber = new BadSubscriber();
            SignalSystem.Subscribe(badSubscriber);

            var ex = Assert.Throws<Exception>(() => SignalSystem.Raise<IBadEvent>(s => s.OnBadEvent()));
            Assert.That(ex.Message, Does.Contain("Event failed"));
        }

        public interface IBadEvent : IGlobalSubscriber
        {
            void OnBadEvent();
        }

        public class BadSubscriber : IBadEvent
        {
            public void OnBadEvent() => throw new InvalidOperationException("fail");
        }
    }
}