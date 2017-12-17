using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitleDatabase.Storage
{
    public class DatabaseStorage : IDatabaseStorage
    {
        private static readonly Dictionary<string, string> Storage = new Dictionary<string, string>();
        
        public Task Delete(string key)
        {
            if (!Storage.ContainsKey(key))
                return Task.CompletedTask;

            lock (key)
            {
                if (Storage.ContainsKey(key))
                {
                    Storage.Remove(key);
                }
            }
            return Task.CompletedTask;
        }

        public Task<string> Read(string key)
        {
            var value = "";
            if (!Storage.ContainsKey(key))
                return Task.FromResult(value);

            lock (key)
            {
                if (Storage.ContainsKey(key))
                {
                    value = Storage[key];
                }
            }
            return Task.FromResult(value);
        }

        public Task Write(string key, string value)
        {
            if (Storage.ContainsKey(key))
            {
                lock (key)
                {
                    if (Storage.ContainsKey(key))
                    {
                        Storage[key] = value;
                        return Task.CompletedTask;
                    }
                }
            }
            lock (key)
            {
                if (!Storage.ContainsKey(key))
                    Storage.Add(key, value);
            }
            return Task.CompletedTask;
        }
    }
}