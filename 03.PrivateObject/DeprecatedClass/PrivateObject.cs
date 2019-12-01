namespace DeprecatedClass
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class PrivateObject
    {
        private readonly object _obj;

        public static MethodInfo GetMethodByName(Type type, string methodName)
        {
            return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        public PrivateObject(object obj)
        {
            this._obj = obj;
        }

        public object Invoke(string methodName, params object[] parameters)
        {
            var method = GetMethodByName(this._obj.GetType(), methodName);

            var result = method.Invoke(this._obj, parameters);

            return result;
        }
    }
}
