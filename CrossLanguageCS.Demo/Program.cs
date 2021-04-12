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

            processor.Table.RegisterFunction("SayHello", SayHello);
            processor.Table.RegisterFunction("PrintHi", PrintHi);

            processor.OnFunctionReceived("SayHello", processor.Parameters.SerialiseParameters("hi there", 5));
            processor.OnFunctionReceived("PrintHi", processor.Parameters.SerialiseParameters());

            Console.Read();
        }

        public static void SayHello(object message, object numberOfTimes)
        {
            int number = (int)numberOfTimes;
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine((string)message);
            }
        }

        public static void PrintHi()
        {
            Console.WriteLine("hiii!");
        }
    }
}
