using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64MemoryModelTestProject.Types
{
    [TestClass]
    public class Address
    {
        [TestMethod]
        public void AddressAsString()
        {
            var a = new C64MemoryModel.Types.Address(1);
            Assert.AreEqual("$0001", a.ToHexString());
            a = new C64MemoryModel.Types.Address(10);
            Assert.AreEqual("$000A", a.ToHexString());
            a = new C64MemoryModel.Types.Address(100);
            Assert.AreEqual("$0064", a.ToHexString());
            a = new C64MemoryModel.Types.Address(1000);
            Assert.AreEqual("$03E8", a.ToHexString());
            a = new C64MemoryModel.Types.Address(10000);
            Assert.AreEqual("$2710", a.ToHexString());
            a = new C64MemoryModel.Types.Address(65535);
            Assert.AreEqual("$FFFF", a.ToHexString());
        }
    }
}