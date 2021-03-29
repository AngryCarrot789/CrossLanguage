using System;
using System.IO.Ports;

// using this from my packet thingy i made a while ago
namespace CrossLanguageCS.FunctionDispatchers.Serial
{
    /// <summary>
    /// A wrapper for the SerialPort class, for sending/receiving raw serial data more efficienctly
    /// </summary>
    public class SerialTransceiver : IDisposable
    {
        public readonly SerialTransmitter Transmitter;
        public readonly SerialReceiver Receiver;
        public readonly SerialPort Port;

        public bool IsConnected
        {
            get => Port.IsOpen;
        }

        public SerialTransceiver(
            string port,
            int baud,
            int dataBits,
            StopBits stopBits,
            Parity parity,
            Action<string> dataReceivedCallback,
            bool startImmidiately = true)
        {
            Port = new SerialPort(port, baud, parity, dataBits, stopBits);
            //Port.ErrorReceived += Port_ErrorReceived;
            //Port.PinChanged += Port_PinChanged;

            Transmitter = new SerialTransmitter(Port);
            Receiver = new SerialReceiver(Port, dataReceivedCallback, startImmidiately);

            if (startImmidiately)
            {
                Connect();
            }
        }

        /// <summary>
        /// Automatically disconnects if connected, or connects if disconnected
        /// </summary>
        public void AutoConnectDisconnect()
        {
            if (IsConnected)
                Disconnect();

            else
                Connect();
        }

        /// <summary>
        /// Connects to the serial port
        /// </summary>
        public void Connect()
        {
            if (IsConnected)
                throw new InvalidOperationException("Already connected to Serial Port on " + Port.PortName);

            if (Port.PortName == "COM1")
                throw new InvalidOperationException("Cannot connect to COM1 as it is a system COM port");

            Port.Open();
        }

        /// <summary>
        /// Disconnects from the serial port
        /// </summary>
        public void Disconnect()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Already disconnected from Serial Port on " + Port.PortName);

            Port.Close();
            Receiver.PauseReceiver();
        }

        /// <summary>
        /// Discards the serial port buffers
        /// </summary>
        /// <param name="read"></param>
        /// <param name="write"></param>
        public void DiscardPortBuffers(bool read, bool write)
        {
            if (read)
                Port.DiscardInBuffer();

            if (write)
                Port.DiscardOutBuffer();
        }

        public void Dispose()
        {
            this.Receiver.Dispose();
            if (this.IsConnected)
                Disconnect();
            this.Port.Dispose();
        }

        //private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        //{
        //    switch (e.EventType)
        //    {
        //        case SerialError.Frame:
        //            Logger.LogLine($"Framing error detected: attempted to read from wrong starting point of data");
        //            Logger.LogLine("Solution: Reset SerialPort");
        //            break;
        //        case SerialError.Overrun:
        //            Logger.LogLine($"Overrun error detected: data arrived before previous data could be processed");
        //            Logger.LogLine("Solution: Reset SerialPort");
        //            break;
        //        case SerialError.RXOver:
        //            Logger.LogLine($"RXOver error detected: the receive buffer is full, or data was received after end-of-file marker");
        //            Logger.LogLine("Solution: Clear Receiver Buffers");
        //            break;
        //        case SerialError.RXParity:
        //            Logger.LogLine($"RXParity error detected: parity might not have been applied, or data was corrupted");
        //            Logger.LogLine("Solution: Restart Application");
        //            break;
        //        case SerialError.TXFull:
        //            Logger.LogLine($"TXFull error detected: attempted to transmit data when output buffer was full");
        //            Logger.LogLine("Solution: Clear Transmit Buffers");
        //            break;
        //    }
        //}
    }
}
