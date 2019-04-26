using System;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _2DGameToolKitTest
{
    [TestClass]
    public class UniqueProxyTest
    {
        class DummyClass
        { }

        class DummyClassProxy : UniqueProxy<DummyClass>
        { }

        private readonly DummyClass m_Dummy = new DummyClass();

        [TestInitialize]
        public void TestInitialize()
        {
            DummyClassProxy.Open(m_Dummy);
        }

        [TestCleanup]
        public void TestCleanup ()
        {
            if (DummyClassProxy.IsValid ())
            {
                DummyClassProxy.Close (m_Dummy);
            }
        }

        [TestMethod]
        public void TestOpen ()
        {
            Assert.IsTrue (DummyClassProxy.IsValid ());
            Assert.AreEqual (m_Dummy, DummyClassProxy.Get ());
        }

        [TestMethod]
        public void TestCloseAfterOpen ()
        {
            DummyClassProxy.Close (m_Dummy);
            Assert.ThrowsException<SecurityException> (delegate { DummyClassProxy.Get (); });
        }

        [TestMethod]
        public void TestCloseBeforeOpen ()
        {
            DummyClassProxy.Close(m_Dummy);
            Assert.ThrowsException<SecurityException> (delegate { DummyClassProxy.Close (m_Dummy); });
        }

        [TestMethod]
        public void TestDoubleOpen ()
        {
            Assert.ThrowsException<SecurityException> (delegate { DummyClassProxy.Open (m_Dummy); });
        }

        public void TestCloseWithWrongProxy()
        {
            DummyClass otherDummy = new DummyClass();
            Assert.ThrowsException<SecurityException>(delegate { DummyClassProxy.Close(otherDummy); });
        }
    }
}
