using System;
using System.Runtime.Serialization;

namespace LitleDatabase.Core.Model
{
    [Serializable]
    public class DatabaseModel : IDatabaseModel
    {
        public DatabaseModel()
        {
        }

        protected DatabaseModel(SerializationInfo info, StreamingContext context)
        {
            var action = (DatabaseAction) info.GetValue(nameof(Action), typeof(DatabaseAction));
            Action = action;
            Key = (string) info.GetValue(nameof(Key), typeof(string));
            Value = (string) info.GetValue(nameof(Value), typeof(string));
        }

        public DatabaseAction Action { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(Action), Action);
            info.AddValue(nameof(Key), Key);
            info.AddValue(nameof(Value), Value);
        }
    }
}