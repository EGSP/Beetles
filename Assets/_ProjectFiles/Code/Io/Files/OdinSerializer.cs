using Egsp.Core;
using Sirenix.OdinSerializer;
using UnityEngine;

namespace Game.Io
{
    public class OdinSerializer : ISerializer
    {
        /// <summary>
        /// Сериализует объект в бинарном формате.
        /// </summary>
        public byte[] Serialize<T>(T obj)
        {
            var serializationData = SerializationUtility.SerializeValue<T>(obj, DataFormat.JSON);
            return serializationData;
        }

        public Option<T> Deserialize<T>(byte[] serializedData)
        {
            var obj = SerializationUtility.DeserializeValue<T>(serializedData, DataFormat.JSON);
            return obj;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void UseThis()
        {
            Storage.SwitchSerializer(new OdinSerializer());
        } 
    }
}