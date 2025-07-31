using NUnit.Framework;
using System.Linq;

namespace GameSignals.Tests.Runtime
{
    public class DummySubscriber { }

    [TestFixture]
    public class SubscribersListTests
    {
        private SubscribersList<DummySubscriber> _list;
        private DummySubscriber _subscriber1;
        private DummySubscriber _subscriber2;

        [SetUp]
        public void SetUp()
        {
            _list = new SubscribersList<DummySubscriber>();
            _subscriber1 = new DummySubscriber();
            _subscriber2 = new DummySubscriber();
        }

        [Test]
        public void Add_Subscriber_IsAdded()
        {
            _list.Add(_subscriber1);
            Assert.Contains(_subscriber1, _list.Subscribers.ToList());
        }

        [Test]
        public void Remove_Subscriber_IsRemoved()
        {
            _list.Add(_subscriber1);
            _list.Remove(_subscriber1);
            Assert.IsFalse(_list.Subscribers.Contains(_subscriber1));
        }

        [Test]
        public void Remove_DuringExecution_MarksForCleanup()
        {
            _list.Add(_subscriber1);
            _list.SetExecuting(true);
            _list.Remove(_subscriber1);
            Assert.IsTrue(_list.Subscribers.Contains(null));
            _list.SetExecuting(false);
            _list.Cleanup();
            Assert.IsFalse(_list.Subscribers.Contains(null));
            Assert.IsFalse(_list.Subscribers.Contains(_subscriber1));
        }

        [Test]
        public void Cleanup_RemovesNulls()
        {
            _list.Add(_subscriber1);
            _list.Add(_subscriber2);
            _list.SetExecuting(true);
            _list.Remove(_subscriber1);
            _list.SetExecuting(false);
            _list.Cleanup();
            Assert.IsFalse(_list.Subscribers.Contains(_subscriber1));
            Assert.Contains(_subscriber2, _list.Subscribers.ToList());
        }

        [Test]
        public void Add_Null_Throws()
        {
            Assert.Throws<System.ArgumentNullException>(() => _list.Add(null));
        }
    }
}