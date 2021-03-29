using System;
using System.IO;
using System.IO.Ports;

// using this from my packet thingy i made a while ago
namespace CrossLanguageCS.FunctionDispatchers.Serial
{
    /// <summary>
    /// A class which is responsible for sending messages through a serial port on 
    /// another thread. It will send the messages in order in which they were asked to be sent
    /// </summary>
    public class SerialTransmitter
    {
        /// <summary>
        /// Whether data can be sent through the serial port or not
        /// </summary>
        public bool CanSend;

        /// <summary>
        /// The serial port :)
        /// </summary>
        private SerialPort Port;

        public SerialTransmitter(SerialPort port)
        {
            Port = port;
            CanSend = true;
        }

        /// <summary>
        /// Sends a message through the <see cref="SerialTransmitter"/>'s <see cref="SerialPort"/> using the
        /// <see cref="SendMessage(string, bool)"/> method
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessageLine(string message)
        {
            return SendMessage(message, true);
        }

        /// <summary>
        /// Sends a message through the <see cref="SerialTransmitter"/>'s <see cref="SerialPort"/>
        /// using the <see cref="SendBytes(byte[], int)"/> method and the <see cref="SerialPort.Encoding"/> encoder
        /// </summary>
        /// <param name="message"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public bool SendMessage(string message, bool newLine = true)
        {
            if (CanSend && Port.IsOpen)
            {
                string newMessage = newLine ? (message + "\n") : message;
                byte[] buffer = Port.Encoding.GetBytes(newMessage);
                SendBytes(buffer, buffer.Length);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes the given <see langword="byte[]"/> buffer to the <see cref="SerialTransmitter"/>'s
        /// <see cref="SerialPort.BaseStream"/> byte-by-byte
        /// </summary>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <param name="buffer"></param>
        /// <param name="amount"></param>
        public void SendBytes(byte[] buffer, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Port.BaseStream.WriteByte(buffer[i]);
            }
        }

        public void SetPort(SerialPort port)
        {
            if (this.Port != null || this.Port.IsOpen)
                return;

            this.Port = port;
        }
    }
}
