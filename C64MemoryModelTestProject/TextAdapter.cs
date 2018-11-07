using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C64MemoryModelTestProject
{
    [TestClass]
    public class TextAdapter
    {
        [TestMethod]
        public void SetByteGetByte()
        {
            var t = new C64MemoryModel.TextAdapter(new C64MemoryModel.Mem.Memory());

            var answer = t.Ask("SETBYTE 1000, 5", out var result);
            Assert.IsTrue(result);
            Assert.AreEqual("01000 $03E8: 000 $00 --> 005 $05", answer);

            answer = t.Ask("GETBYTE 1000", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("01000 $03E8: 005 $05", answer);

            answer = t.Ask("GETBYTE 999", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("00999 $03E7: 000 $00", answer);

            answer = t.Ask("GETBYTE", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("01000 $03E8: 005 $05", answer);

            answer = t.Ask("SETBYTE 6", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("01001 $03E9: 000 $00 --> 006 $06", answer);

            answer = t.Ask("GETBYTE 1000", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("01000 $03E8: 005 $05", answer);

            answer = t.Ask("GETBYTE", out result);
            Assert.IsTrue(result);
            Assert.AreEqual("01001 $03E9: 006 $06", answer);
        }
    }
}