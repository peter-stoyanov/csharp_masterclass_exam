namespace Calculator.Tests
{
    using CTF.Framework.Asserts;
    using CTF.Framework.Attributes;

    public class CalculatorTests2
    {
        public void ShouldReturnTrueWhenTwoIntegersAreEqual2()
        {
            CTFAssert.AreEqual(1, 1);
        }
    }
}