using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitleDatabase.Core.Storage
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
                    //File.AppendAllText("log/logs.txt", Environment.NewLine + $"{DateTime.UtcNow:HH:mm:ss} record with key: {key} Delete  {Storage.Count} {Name}");
                    //Console.WriteLine($"record with key: {key} deleted");
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

            //Console.WriteLine($"record with key: {key} read, value: {value}");
            //File.AppendAllText("log/logs.txt", Environment.NewLine + $"{DateTime.UtcNow:HH:mm:ss} record with key: {key} Read  {Storage.Count} {Name}");
            return Task.FromResult(value);
        }

        public Task Write(string key, string value)
        {
            //Console.WriteLine($"record with key: {key} updated, value: {value}");
            //File.AppendAllText("log/logs.txt", Environment.NewLine + $"{DateTime.UtcNow:HH:mm:ss} record with key: {key} Write  {Storage.Count} {Name}");

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