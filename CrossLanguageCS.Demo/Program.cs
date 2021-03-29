using System;
using CrossLanguageCS.FunctionDispatchers;
using CrossLanguageCS.Functions;

namespace CrossLanguageCS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionProcessor processor = new SerialFunctionProcessor();

            Console.WriteLine("Hello World!");
        }
    }
}
