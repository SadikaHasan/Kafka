using MessagePack;

namespace ConsoleKafka.Models
{
    [MessagePackObject]
    public class PuChatMessage
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public string Value { get; set; } = string.Empty;
        [Key(2)]
        public string Sender { get; set; } = string.Empty;
    }
}
