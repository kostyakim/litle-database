using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LitleDatabase.Core
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T ToObject<T>(this byte[] data)
        {
            if (data == null)
                return default(T);

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var obj = bf.Deserialize(ms);
                return (T) obj;
            }
        }
    }
}