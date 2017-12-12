using System.Runtime.Serialization;

namespace LitleDatabase.Core.Model
{
    public interface IDatabaseModel : ISerializable
    {
        DatabaseAction Action { get; }
        string Value { get; }
        string Key { get; }
    }
}