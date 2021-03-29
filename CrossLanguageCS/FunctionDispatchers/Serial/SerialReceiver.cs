using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

// using this from my packet thingy i made a while ago
namespace CrossLanguageCS.FunctionDispatchers.Serial
{
    /// <summary>
    /// A class for listening to data being avaliable on a <see cref="SerialPort"/> using another
    /// <see cref="Thread"/>, and calling a callback function (<see cref="Action{string}"/>) when a line of data is received
    /// </summary>
    public class SerialReceiver : IDisposable
    {
        private bool IsEnabled;
        private bool ShouldFullyShutdownThread;
        private readonly Thread ReceiverThread;
        private readonly SerialPort Port;

        private Action<string> MessageReceivedCallback;

        public SerialReceiver(SerialPort port, Action<string> dataReceivedCallback, bool startImmediately = true)
        {
            MessageReceivedCallback = dataReceivedCallback;
            Port = port;
            IsEnabled = startImmediately;
            ReceiverThread = new Thread(ReceiverMain);
            ReceiverThread.Start();
        }

        /// <summary>
        /// Allows data (from the <see cref="SerialPort"/>'s input buffer) to be processed
        /// </summary>
        public void StartReceiver()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// Stops processing data in the <see cref="SerialPort"/>'s input buffer. Doesn't stop the receiver thread
        /// </summary>
        public void PauseReceiver()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Returns any data still remaining in the buffer on the port that the <see cref="SerialReceiver"/> is opened on
        /// </summary>
        /// <returns>the buffered data up to the new line character</returns>
        public string GetBufferedData()
        {
            StringBuilder buffer = new StringBuilder(Port.BytesToRead);
            char read;
            if (Port.IsOpen)
            {
                while (Port.BytesToRead > 0)
                {
                    read = (char)Port.ReadChar();
                    switch (read)
                    {
                        case '\r':
                            break;
                        case '\n':
                            break;
                        default:
                            buffer.Append(read);
                            break;
                    }
                }
            }
            return buffer.ToString();
        }

        public void ClearBufferedData()
        {
            GetBufferedData();
        }

        /// <summary>
        /// The main function for the receiver thread
        /// </summary>
        private void ReceiverMain()
        {
            // 512 chars (1024 bytes in memory... probably)
            StringBuilder buffer = new StringBuilder(512);
            char nextChar;

            while (true)
            {
                if (ShouldFullyShutdownThread)
                {
                    IsEnabled = false;
                    return;
                }

                if (IsEnabled)
                {
                    if (Port == null)
                    {
                        Thread.Sleep(10);
                    }
                    else
                    {
                        if (Port.IsOpen)
                        {
                            while (Port.BytesToRead > 0)
                            {
                                if (IsEnabled)
                                {
                                    nextChar = (char)Port.ReadChar();
                                    switch (nextChar)
                                    {
                                        case '\r':
                                            break;
                                        case '\n':
                                            MessageReceivedCallback(buffer.ToString());
                                            buffer.Clear();
                                            break;
                                        default:
                                            buffer.Append(nextChar);
                                            break;
                                    }
                                }
                                else
                                {
                                    Thread.Sleep(10);
                                }
                            }
                        }
                        Thread.Sleep(5);
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        /// Fully kills the thread. Only use when shutting down the app
        /// </summary>
        private void KillThreadLoop()
        {
            IsEnabled = false;
            ShouldFullyShutdownThread = true;
            ReceiverThread.Abort();
        }

        public void Dispose()
        {
            KillThreadLoop();
            this.MessageReceivedCallback = null;
        }
    }
}
