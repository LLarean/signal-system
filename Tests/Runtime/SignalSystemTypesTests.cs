using System;
using NUnit.Framework;

namespace GameSignals.Tests.Runtime
{
    public interface ITestSubscriber : IGlobalSubscriber { }
    public interface IOtherSubscriber : IGlobalSubscriber { }
    public class TestClass : ITestSubscriber, IOtherSubscriber { }

    [TestFixture]
    public class SignalSystemTypesTests
    {
        [Test]
        public void GetSubscriberTypes_ReturnsAllIGlobalSubscriberInterfaces()
        {
            var obj = new TestClass();
            var types = SignalSystemTypes.GetSubscriberTypes(obj);

            Assert.That(types, Contains.Item(typeof(ITestSubscriber)));
            Assert.That(types, Contains.Item(typeof(IOtherSubscriber)));
            Assert.That(types, Is.All.AssignableTo<Type>());
            Assert.That(types, Has.Count.EqualTo(2));
        }

        [Test]
        public void GetSubscriberTypes_CachesResult()
        {
            var obj = new TestClass();
            var types1 = SignalSystemTypes.GetSubscriberTypes(obj);
            var types2 = SignalSystemTypes.GetSubscriberTypes(obj);

            Assert.That(ReferenceEquals(types1, types2), Is.True);
        }

        [Test]
        public void GetSubscriberTypes_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SignalSystemTypes.GetSubscriberTypes(null));
        }
    }
}