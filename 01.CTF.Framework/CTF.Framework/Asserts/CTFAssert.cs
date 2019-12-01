namespace CTF.Framework.Asserts
{
    using CTF.Framework.Exceptions;
    using System;

    // ReSharper disable once InconsistentNaming
    public abstract class CTFAssert
    {
        public static void AreEqual(object a, object b)
        {
            if (!a.Equals(b))
            {
                throw new TestException();
            }
        }

        public static void AreNotEqual(object a, object b)
        {
            if (a.Equals(b))
            {
                throw new TestException();
            }
        }

        public static void Throws<T>(Func<bool> condition)
        {
            // throw new NotImplementedException();
        }
    }
}
