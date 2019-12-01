namespace CTF.Framework.Attributes
{
    using System;

    // ReSharper disable once InconsistentNaming
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CTFTestMethodAttribute : Attribute
    {
    }
}
