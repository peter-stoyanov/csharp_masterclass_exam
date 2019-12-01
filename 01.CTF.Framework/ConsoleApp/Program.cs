namespace ConsoleApp
{
    using System;
    using System.Reflection;
    using Calculator.Tests;
    using CTF.Framework.TestRunner;

    public class Program
    {
        public static void Main(string[] args)
        {
            var ass = Assembly.GetAssembly(typeof(CalculatorTests));

            string assemblyPath = ass.Location;
            Runner runner = new Runner();
            string result = runner.Run(assemblyPath);
            Console.WriteLine(result);
        }
    }
}
