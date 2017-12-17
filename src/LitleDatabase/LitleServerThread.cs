using System;
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

        public LitleServerThread(int port, IServerClient client)
        {
            var localAddr = IPAddress.Parse("127.0.0.1");
            _tcpListener = new TcpListener(localAddr, port);
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
            var buffer = new byte[client.ReceiveBufferSize];
            stream.Read(buffer, 0, client.ReceiveBufferSize);

            var result = _serverClient.Call(buffer).GetAwaiter().GetResult();
            // отправка сообщения
            stream.Write(result, 0, result.Length);
            // закрываем подключение
            client.Close();}
    }
}