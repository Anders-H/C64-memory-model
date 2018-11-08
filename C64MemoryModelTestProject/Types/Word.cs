using C64MemoryModel.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64MemoryModelTestProject.Types
{
    [TestClass]
    public class Word
    {
        [TestMethod]
        public void FromUshort()
        {
            using (var x = new C64MemoryModel.Types.Word(255))
            {
                Assert.AreEqual(255, x.Value, "To ushort");
                Assert.AreEqual(0, x.HighByte.Value, "To high byte");
                Assert.AreEqual(255, x.LowByte.Value, "To low byte");
                Assert.AreEqual("$00FF", x.ToHexString(), "To hex");
                Assert.AreEqual("00255", x.ToString(), "To string");
                Assert.AreEqual("0000000011111111", x.ToBinString(), "To binary");
            }
            using (var x = new C64MemoryModel.Types.Word(256))
            {
                Assert.AreEqual(256, x.Value, "To ushort");
                Assert.AreEqual(1, x.HighByte.Value, "To high byte");
                Assert.AreEqual(0, x.LowByte.Value, "To low byte");
                Assert.AreEqual("$0100", x.ToHexString(), "To hex");
                Assert.AreEqual("00256", x.ToString(), "To string");
                Assert.AreEqual("0000000100000000", x.ToBinString(), "To binary");
            }
        }

        [TestMethod]
        public void FromBytesWithBitModification()
        {
            using (var x = new C64MemoryModel.Types.Word(new Byte(41), new Byte(191)))
            {
                Assert.AreEqual(10687, x.Value, "To ushort");
                Assert.AreEqual(41, x.HighByte.Value, "To high byte");
                Assert.AreEqual(191, x.LowByte.Value, "To low byte");
                Assert.AreEqual("$29BF", x.ToHexString(), "To hex");
                Assert.AreEqual("10687", x.ToString(), "To string");
                Assert.AreEqual("0010100110111111", x.ToBinString(), "To binary");
                x.HighByte.Bit6 = true;
                x.LowByte.Bit5 = false;
                Assert.AreEqual(27039, x.Value, "To ushort");
                Assert.AreEqual(105, x.HighByte.Value, "To high byte");
                Assert.AreEqual(159, x.LowByte.Value, "To low byte");
                Assert.AreEqual("$29BF", x.ToHexString(), "To hex");
                Assert.AreEqual("10687", x.ToString(), "To string");
                Assert.AreEqual("0110100110011111", x.ToBinString(), "To binary");
            }
        }
    }
}