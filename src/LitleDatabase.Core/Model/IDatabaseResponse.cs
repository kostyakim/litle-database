using System.Runtime.Serialization;

namespace LitleDatabase.Core.Model
{
    public interface IDatabaseResponse : ISerializable
    {
        bool IsSuccess { get; set; }
        string Value { get; set; }
    }
}