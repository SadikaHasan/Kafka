using Confluent.Kafka;
using ConsoleKafka.Models;
using ConsoleKafka.Serialization;

namespace ConsoleKafka.Kafka
{
    public class KafkaConsumer
    {
        private static IConsumer<Guid, PuChatMessage> _consumer;
        private readonly string user;
        private bool _running = true;

        public KafkaConsumer(string user)
        {
            var cfg = new ConsumerConfig()
            {
                BootstrapServers = "pkc-7xoy1.eu-central-1.aws.confluent.cloud:9092",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslUsername = "YWULFRPB3FUBKXZ6",
                SaslPassword = "3xYVjpimzsKS+XK5lYUYpG2kkQx7SIUTMFtMUdqwBJuocQWa4BzyCBbEOJroNVBf",
                SaslMechanism = SaslMechanism.Plain,
                GroupId = $"Sadika.{Guid.NewGuid()}",
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            _consumer = new ConsumerBuilder<Guid, PuChatMessage>(cfg)
                .SetKeyDeserializer(new MessagePackDeserializer<Guid>())
                .SetValueDeserializer(new MessagePackDeserializer<PuChatMessage>())
                .Build();

            var topics = new List<string>()
            {
                "pu-chat"
            };

            _consumer.Subscribe(topics);
            this.user = user;
        }

       public Task Consume()
        {
            while (_running)
            {
                var result = _consumer.Consume();
                if(result.Message==null)
                {
                    continue;
                }
                var msg = result.Message.Value;
                Console.WriteLine($"[{msg.Sender}] {msg.Value}");
            }
            return Task.CompletedTask;
          
        }

    }
}
