using System;
using System.Threading.Tasks;
using LitleDatabase.Core;
using LitleDatabase.Core.Model;
using LitleDatabase.Storage;

namespace LitleDatabase.Server
{
    public class DatabaseServerClient : IServerClient
    {
        private readonly IDatabaseStorage _storage;

        public DatabaseServerClient()
        {
            _storage = new DatabaseStorage();
        }

        public async Task<byte[]> Call(byte[] byteArray)
        {
            var obj = byteArray.ToObject<IDatabaseModel>();
            try
            {
                switch (obj.Action)
                {
                    case DatabaseAction.Delete:
                        await _storage.Delete(obj.Key);
                        return new DatabaseResponse {IsSuccess = true}.ToByteArray();
                    case DatabaseAction.AddOrUpdate:
                        await _storage.Write(obj.Key, obj.Value);
                        return new DatabaseResponse {IsSuccess = true}.ToByteArray();
                    case DatabaseAction.Get:
                        var value = await _storage.Read(obj.Key);
                        return new DatabaseResponse {IsSuccess = true, Value = value}.ToByteArray();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                return new DatabaseResponse {IsSuccess = false}.ToByteArray();
            }
        }
    }
}