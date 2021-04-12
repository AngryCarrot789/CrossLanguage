using System;
using CrossLanguageCS.FunctionDispatchers;
using CrossLanguageCS.Functions;

namespace CrossLanguageCS.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialFunctionProcessor processor = new SerialFunctionProcessor();

            processor.Register.RegisterFunction("SayHello", SayHello);
            processor.Register.RegisterFunction("WriteConsole", PrintHi);

            //Console.Read();

            processor.Dispatcher.DispatchFunction("writeConsole", "hello from C#");

            // processor.OnFunctionReceived("SayHello", processor.Parameters.SerialiseParameters("hi there", 5));
            // processor.OnFunctionReceived("PrintHi", processor.Parameters.SerialiseParameters());

        }

        public static void SayHello(object message, object numberOfTimes)
        {
            int number = (int)numberOfTimes;
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine((string)message);
            }
        }

        public static void PrintHi(object content)
        {
            Console.WriteLine(content.ToString());
        }
    }
}
