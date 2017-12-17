using System.Threading.Tasks;

namespace LitleDatabase.Server
{
    public interface IServerClient
    {
        Task<byte[]> Call(byte[] byteArray);
    }
}