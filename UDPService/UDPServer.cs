using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPService
{
    public class UDPServer
    {
        public OnMessageRecivedDelegate OnMassegeRecieved = null;

        public const int PORT = 5000;

        private Socket _socket;
        private EndPoint _ep;

        private byte[] _buffer_recv;
        private ArraySegment<byte> _buffer_recv_segment;

        public void Initialize()
        {
            _buffer_recv = new byte[4096];
            _buffer_recv_segment = new ArraySegment<byte>(_buffer_recv);

            _ep = new IPEndPoint(IPAddress.Any, PORT);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            _socket.Bind(_ep);
        }

        public void StartMessageLoop()
        {
            _ = Task.Run(async () =>
            {
                SocketReceiveMessageFromResult res;
                while (true)
                {
                    res = await _socket.ReceiveMessageFromAsync(_buffer_recv_segment, SocketFlags.None, _ep);
                    var msg = Encoding.UTF8.GetString(_buffer_recv, 0, res.ReceivedBytes);
                    
                    if(OnMassegeRecieved != null)
                    {
                        await OnMassegeRecieved(msg);
                    }
                }
            });
        }

        public void Close()
        {
            _socket.Close();
        }

        //public async Task SendTo(EndPoint recipient, byte[] data)
        //{
        //    var s = new ArraySegment<byte>(data);
        //    await _socket.SendToAsync(s, SocketFlags.None, recipient);
        //}
    }

    public delegate Task OnMessageRecivedDelegate(string message);
}
