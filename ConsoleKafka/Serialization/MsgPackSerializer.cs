﻿using Confluent.Kafka;
using MessagePack;

namespace ConsoleKafka.Serialization
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
