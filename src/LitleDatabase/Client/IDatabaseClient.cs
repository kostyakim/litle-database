using System;
using System.Threading.Tasks;

namespace LitleDatabase.Client
{
    public interface IDatabaseClient : IDisposable
    {
        Task<string> Get(string key);
        Task Post(string key, string value);
        Task Delete(string key);
    }
}