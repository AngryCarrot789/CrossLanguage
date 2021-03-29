using System.IO.Ports;
using CrossLanguageCS.FunctionDispatchers.Serial;
using CrossLanguageCS.Functions;

namespace CrossLanguageCS.FunctionDispatchers
{
    public class SerialFunctionDispatcher : FunctionProcessor
    {
        private SerialTransceiver Transceiver;

        public SerialFunctionDispatcher()
        {
            this.Transceiver = new SerialTransceiver("COM20", 9600, 8, StopBits.One, Parity.None, OnLineReceived, true);
        }

        private void OnLineReceived(string line)
        {
            // received:
            // sendPing:s'www.google.co.uk',i1000
        }
    }
}
