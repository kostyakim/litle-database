using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LitleDatabase.Server;

namespace LitleDatabase
{
    public class LitleServerThread : IDisposable
    {
        private readonly IServerClient _serverClient;
        private readonly TcpListener _tcpListener;

        public LitleServerThread(IPAddress address, int port, IServerClient client)
        {
            _tcpListener = new TcpListener(address, port);
            _serverClient = client;
        }

        public void Dispose()
        {
            _tcpListener.Stop();
        }

        public void Start()
        {
            _tcpListener.Start();
            while (true)
            {
                ThreadPool.QueueUserWorkItem(ClientThread, _tcpListener.AcceptTcpClient());
            }
        }

        private void ClientThread(object stateInfo)
        {
            // Просто создаем новый экземпляр класса Client и передаем ему приведенный к классу TcpClient объект StateInfo
            var client = (TcpClient)stateInfo;
            // получаем сетевой поток для чтения и записи
            var stream = client.GetStream();
            var data = new List<byte>();
            var bytes = 0;
            do
            {
                var buffer = new byte[client.ReceiveBufferSize];
                stream.Read(buffer, bytes, client.ReceiveBufferSize);
                data.AddRange(buffer);
            }
            while (stream.DataAvailable);

            var result = _serverClient.Call(data.ToArray()).GetAwaiter().GetResult();
            // отправка сообщения
            stream.Write(result, 0, result.Length);
            // закрываем подключение
            client.Close();}
    }
}