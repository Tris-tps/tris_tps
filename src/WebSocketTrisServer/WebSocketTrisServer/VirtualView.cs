using System.Diagnostics;
using System.Net.Sockets;
using System.Text.Json;

namespace WebSocketTrisServer
{
    public class VirtualView
    {
        private StreamReader _reader;
        private StreamWriter _writer;

        private Dictionary<Message.Code, Action<VirtualView, Message>> callbacks;

        public VirtualView(Socket socket)
        {
            _reader = new StreamReader(new NetworkStream(socket));
            _writer = new StreamWriter(new NetworkStream(socket));
            _writer.AutoFlush = true;

            callbacks = new Dictionary<Message.Code, Action<VirtualView, Message>>();
        }

        /// <summary>
        /// Adds a collback which will be called when the server receive a message
        /// </summary>
        public void AddCallback(Message.Code code, Action<VirtualView, Message> callback)
        {
            callbacks.Add(code, callback);
        }

        public void Run()
        {
            while (true)
            {
                var msg = JsonSerializer.Deserialize<Message>(_reader.ReadLine());
                Debug.Assert(callbacks.ContainsKey(msg.MessageCode));
                callbacks[msg.MessageCode](this, msg);
            }
        }

        public void SendMessage(Message msg)
        {
            _writer.WriteLine(JsonSerializer.Serialize(msg));
        }
    }
}
