using Confluent.Kafka;
using ConsoleKafka.Kafka;

namespace ConsoleKafka
{
    internal class Program
    {
        private static KafkaConsumer _consumer;
        private static KafkaProducer _producer;
        private static string _user;

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name:");
            var user = Console.ReadLine();
            
            var _consumer = new KafkaConsumer(user);
            var _producer = new KafkaProducer(user);

            Task.Run(() => { _consumer.Consume(); });

            var isRunning = true;
            do
            {
                var msg = Console.ReadLine();
                
                if (msg == "/exit")
                {
                    break;
                }

                else
                {
                    _producer.ProduceMessage(msg);
                }
            } while (isRunning);


            Console.WriteLine("Goodbye!");
        }
       

    }
}
