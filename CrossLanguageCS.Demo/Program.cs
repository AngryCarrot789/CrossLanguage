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

            processor.Table.RegisterFunction<string, int>("SayHello", SayHello);

            processor.OnFunctionReceived("SayHello", processor.Parameters.SerialiseParameters("hi there", 5));

            Console.Read();
        }

        public static void SayHello(string message, int numberOfTimes)
        {
            for(int i = 0; i < numberOfTimes; i++)
            {
                Console.WriteLine(message);
            }
        }
    }
}
