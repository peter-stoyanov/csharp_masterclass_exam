namespace CTF.Framework.TestRunner
{
    using CTF.Framework.Attributes;
    using CTF.Framework.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Runner
    {
        private readonly StringBuilder stringBuilder;

        public static IEnumerable<Type> GetTypesWithAttribute(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(CTFTestClassAttribute), true).Length > 0)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type type)
        {
            return type.GetMethods()
                      .Where(m => m.GetCustomAttributes(typeof(CTFTestMethodAttribute), false).Length > 0)
                      .ToArray();
        }

        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public Runner()
        {
            this.stringBuilder = new StringBuilder();
        }

        public string Run(string assemblyPath)
        {
            try
            {
                var sutAssembly = Assembly.LoadFrom(assemblyPath);

                var sutTypes = GetTypesWithAttribute(sutAssembly);

                foreach (Type sutType in sutTypes)
                {
                    // Console.WriteLine(sutType.Name);
                    var sutObject = CreateInstance(sutType);

                    foreach (MethodInfo method in GetMethodsWithAttribute(sutType))
                    {
                        // Console.WriteLine(method.Name);
                        try
                        {
                            object result = method.Invoke(sutObject, new object[] { });

                            this.stringBuilder.AppendLine($"Class: {sutType.Name} Method: {method.Name} - passed!");
                        }
                        catch (TargetInvocationException testEx)
                        {
                            this.stringBuilder.AppendLine($"Class: {sutType.Name} Method: {method.Name} - failed!");
                        }
                        catch (Exception)
                        {
                            // exception in the tested code
                            this.stringBuilder.AppendLine($"Unexpected error occurred in {method.Name}!");
                        }
                    }
                }

                return this.stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                // exception in the framework code
                return ex.ToString();
            }
        }
    }
}
