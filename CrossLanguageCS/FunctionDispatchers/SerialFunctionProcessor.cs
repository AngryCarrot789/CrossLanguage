using System;
using System.Collections.Generic;
using System.IO.Ports;
using CrossLanguageCS.FunctionDispatchers.Serial;
using CrossLanguageCS.Functions;

namespace CrossLanguageCS.FunctionDispatchers
{
    public class SerialFunctionProcessor : FunctionProcessor
    {
        private readonly SerialTransceiver Transceiver;

        public SerialFunctionProcessor()
        {
            this.Transceiver = new SerialTransceiver("COM20", 9600, 8, StopBits.One, Parity.None, OnLineReceived, true);
        }

        private void OnLineReceived(string line)
        {
            // received:
            // sendPing:s'www.google.co.uk',i1000

            // pair.Key == "sendPing"
            // pair.Value == "s'www.google.co.uk',i1000"
            KeyValuePair<string, string> pair = base.Parameters.SplitNameAndParameters(line);
            if (pair.Key == null)
            {
                Console.WriteLine("Failed to receive a function invokation: the function name was null");
                return;
            }
            if (pair.Value == null)
            {
                Console.WriteLine("Failed to receive a function invokation: the parameter was null");
                return;
            }

            base.OnFunctionReceived(pair.Key, pair.Value);
        }
    }
}
