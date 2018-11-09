using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64MemoryModelTestProject.Types
{
	[TestClass]
	public class Address
	{
		[TestMethod]
		public void AddressAsString()
		{
			using (var a = new C64MemoryModel.Types.Address(1))
				Assert.AreEqual("$0001", a.ToHexString());
			using (var a = new C64MemoryModel.Types.Address(10))
				Assert.AreEqual("$000A", a.ToHexString());
			using (var a = new C64MemoryModel.Types.Address(100))
				Assert.AreEqual("$0064", a.ToHexString());
			using (var a = new C64MemoryModel.Types.Address(1000))
				Assert.AreEqual("$03E8", a.ToHexString());
			using (var a = new C64MemoryModel.Types.Address(10000))
				Assert.AreEqual("$2710", a.ToHexString());
			using (var a = new C64MemoryModel.Types.Address(65535))
				Assert.AreEqual("$FFFF", a.ToHexString());
		}

		[TestMethod]
		public void IncreaseAndDecrease()
		{
			using (var a = new C64MemoryModel.Types.Address(255))
			{
				Assert.AreEqual(255, a.Value, "To ushort");
				Assert.AreEqual(0, a.HighByte.Value, "To high byte");
				Assert.AreEqual(255, a.LowByte.Value, "To low byte");
				Assert.AreEqual("$00FF", a.ToHexString(), "To hex");
				Assert.AreEqual("00255", a.ToString(), "To string");
				Assert.AreEqual("0000000011111111", a.ToBinString(), "To binary");
				Assert.IsTrue(a.CanInc(2), "Can increase");
				a.Inc(2);
				Assert.AreEqual(257, a.Value, "To ushort");
				Assert.AreEqual(1, a.HighByte.Value, "To high byte");
				Assert.AreEqual(1, a.LowByte.Value, "To low byte");
				Assert.AreEqual("$0101", a.ToHexString(), "To hex");
				Assert.AreEqual("00257", a.ToString(), "To string");
				Assert.AreEqual("0000000100000001", a.ToBinString(), "To binary");
				Assert.IsTrue(a.CanDec(), "Can decrease");
				a.Dec();
				Assert.AreEqual(256, a.Value, "To ushort");
				Assert.AreEqual(1, a.HighByte.Value, "To high byte");
				Assert.AreEqual(0, a.LowByte.Value, "To low byte");
				Assert.AreEqual("$0100", a.ToHexString(), "To hex");
				Assert.AreEqual("00256", a.ToString(), "To string");
				Assert.AreEqual("0000000100000000", a.ToBinString(), "To binary");
			}
		}
	}
}