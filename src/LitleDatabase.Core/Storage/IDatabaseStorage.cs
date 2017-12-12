using System.Threading.Tasks;

namespace LitleDatabase.Core.Storage
{
    public interface IDatabaseStorage
    {
        Task<string> Read(string key);
        Task Write(string key, string value);
        Task Delete(string key);
    }
}