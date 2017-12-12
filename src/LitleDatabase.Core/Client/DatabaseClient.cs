using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using LitleDatabase.Core.Model;

namespace LitleDatabase.Core.Client
{
    public class DatabaseClient : IDatabaseClient
    {
        private readonly TcpClient _tcpClient;

        public DatabaseClient(string host, int port)
        {
            _tcpClient = new TcpClient(host, port)
            {
                SendTimeout = TimeSpan.FromSeconds(2).Milliseconds,
                ReceiveTimeout = TimeSpan.FromSeconds(2).Milliseconds
            };
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }

        public Task<string> Get(string key)
        {
            var nwStream = _tcpClient.GetStream();
            var model = new DatabaseModel {Action = DatabaseAction.Get, Key = key};
            var bytesToSend = model.ToByteArray();
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            var bytesToRead = new byte[_tcpClient.ReceiveBufferSize];
            nwStream.Read(bytesToRead, 0, _tcpClient.ReceiveBufferSize);
            var readModel = bytesToRead.ToObject<IDatabaseResponse>();
            return Task.FromResult(readModel.Value);
        }

        public Task Post(string key, string value)
        {
            var nwStream = _tcpClient.GetStream();
            var model = new DatabaseModel {Action = DatabaseAction.AddOrUpdate, Value = value, Key = key};
            var bytesToSend = model.ToByteArray();
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            return Task.CompletedTask;
        }

        public Task Delete(string key)
        {
            var nwStream = _tcpClient.GetStream();
            var model = new DatabaseModel {Action = DatabaseAction.Delete, Key = key};
            var bytesToSend = model.ToByteArray();
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            return Task.CompletedTask;
        }
    }
}