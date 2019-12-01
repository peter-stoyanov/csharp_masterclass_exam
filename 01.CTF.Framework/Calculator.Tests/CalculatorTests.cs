namespace Calculator.Tests
{
    using CTF.Framework.Asserts;
    using CTF.Framework.Attributes;

    [CTFTestClass]
    public class CalculatorTests
    {
        [CTFTestMethod]
        public void ShouldReturnTrueWhenTwoIntegersAreEqual()
        {
            CTFAssert.AreEqual(1, 1);
        }

        [CTFTestMethod]
        public void ShouldReturnTrueWhenTwoIntegersAreNotEqual()
        {
            CTFAssert.AreNotEqual(1, 1);
        }
    }
}