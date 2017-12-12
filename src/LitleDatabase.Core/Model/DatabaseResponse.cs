using System;
using System.Runtime.Serialization;

namespace LitleDatabase.Core.Model
{
    [Serializable]
    public class DatabaseResponse : IDatabaseResponse, ISerializable
    {
        public DatabaseResponse() { }

        protected DatabaseResponse(SerializationInfo info, StreamingContext context)
        {
            IsSuccess = (bool)info.GetValue(nameof(IsSuccess), typeof(bool));
            Value = (string)info.GetValue(nameof(Value), typeof(string));
        }

        public bool IsSuccess { get; set; }
        public string Value { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            
            info.AddValue(nameof(IsSuccess), IsSuccess);
            info.AddValue(nameof(Value), Value);
        }
    }
}