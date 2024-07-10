using Confluent.Kafka;
using ConsoleKafka.Models;
using ConsoleKafka.Serialization;

namespace ConsoleKafka.Kafka
{
    public class KafkaProducer
    {
        private static IProducer<Guid, PuChatMessage> _producer;
        private readonly string _user;

        public KafkaProducer(string user)
        {

            var config = new ProducerConfig()
            {
                BootstrapServers = "pkc-7xoy1.eu-central-1.aws.confluent.cloud:9092",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = "YWULFRPB3FUBKXZ6",
                SaslPassword = "3xYVjpimzsKS+XK5lYUYpG2kkQx7SIUTMFtMUdqwBJuocQWa4BzyCBbEOJroNVBf",
                SaslMechanism = SaslMechanism.Plain
            };

            _producer = new ProducerBuilder<Guid, PuChatMessage>(config)
                .SetKeySerializer(new MsgPackSerializer<Guid>())
                .SetValueSerializer(new MsgPackSerializer<PuChatMessage>())
            .Build();
            _user = user;
        }
        public void ProduceMessage(string Text)
        {
            var msg = new Message<Guid, PuChatMessage>()
            {
                Key = Guid.NewGuid(),
                Value = new PuChatMessage {
                    Value = Text,
                    Id = Guid.NewGuid(),
                    Sender = _user,
                },
            };

            _producer.Produce("pu-chat", msg);
        }
    }
    
}
