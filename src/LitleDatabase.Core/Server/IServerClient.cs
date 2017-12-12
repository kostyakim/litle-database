using System.Threading.Tasks;

namespace LitleDatabase.Core.Server
{
    public interface IServerClient
    {
        Task<byte[]> Call(byte[] byteArray);
    }
}