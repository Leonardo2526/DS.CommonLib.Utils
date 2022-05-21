using DS.MainUtils.Strings;

namespace DS.MainUtils.Test
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow()]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("123")]
        [DataRow("10")]
        [DataRow("$%")]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("0.4234234")]
        [DataRow("!aAjhjGH436")]
        [DataRow("a_")]
        [DataRow("a")]
        [DataRow("aAjhjGH436")]
        public void IsNoSpecialSymbol(string s)
        {
            bool check = s.IsNoSpecialSymbol();
            Assert.IsTrue(check);
        }

        [DataTestMethod]
        [DataRow("a")]
        [DataRow("10")]
        [DataRow("$%")]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("0.4234234")]
        public void IsNumberTest(string s)
        {
            bool check = s.IsNumber();
            Assert.IsTrue(check);
        }

        [DataTestMethod]
        [DataRow("a")]
        [DataRow("10")]
        [DataRow("$%")]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("0.4234234")]
        public void IsNaturalNumberTest(string s)
        {
            bool check = s.IsNaturalNumber();
            Assert.IsTrue(check);
        }

        [DataTestMethod]
        [DataRow("a")]
        [DataRow("10")]
        [DataRow("$%")]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("0.4234234")]
        public void IsIntTest(string s)
        {
            bool check = s.IsInt();
            Assert.IsTrue(check);
        }
    }
}