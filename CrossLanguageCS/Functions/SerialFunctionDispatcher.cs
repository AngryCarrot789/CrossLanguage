using System;
using System.Collections.Generic;
using System.Text;
using CrossLanguageCS.FunctionDispatchers.Serial;

namespace CrossLanguageCS.Functions
{
    public class SerialFunctionDispatcher : IFunctionDispatcher
    {
        private readonly ParameterParser Parser;
        private readonly SerialTransceiver Transceiver;

        public SerialFunctionDispatcher(SerialTransceiver transceiver, ParameterParser parser)
        {
            this.Parser = parser;
            this.Transceiver = transceiver;
        }

        public void DispatchFunction(string name)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters()));
        }

        public void DispatchFunction<T1>(string name, T1 a)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a)));
        }

        public void DispatchFunction<T1, T2>(string name, T1 a, T2 b)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a, b)));
        }

        public void DispatchFunction<T1, T2, T3>(string name, T1 a, T2 b, T3 c)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a, b, c)));
        }

        public void DispatchFunction<T1, T2, T3, T4>(string name, T1 a, T2 b, T3 c, T4 d)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a, b, c, d)));
        }

        public void DispatchFunction<T1, T2, T3, T4, T5>(string name, T1 a, T2 b, T3 c, T4 d, T5 e)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a, b, c, d, e)));
        }

        public void DispatchFunction<T1, T2, T3, T4, T5, T6>(string name, T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
        {
            Transceiver.Transmitter.SendMessageLine(Parser.JoinFunction(name, Parser.SerialiseParameters(a, b, c, d, e, f)));
        }
    }
}
