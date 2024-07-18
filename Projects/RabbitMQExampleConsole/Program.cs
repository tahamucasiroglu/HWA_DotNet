using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        //SimpleExample();
        DirectExchangeExample();
    }

    private static void DirectExchangeExample()
    {
        DirectExchangePublisher();
        DirectExchangeConsumer();
    }

    private static void DirectExchangePublisher()
    {
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

        for (int i = 0; i < 100; i++)
        {
            Console.Write("Mesaj : ");
            byte[] message = Encoding.UTF8.GetBytes($"Mesaj {i}");
            channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: message);
        }
    }

    private static void DirectExchangeConsumer()
    {
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

        string queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");

        EventingBasicConsumer consumer = new(channel);
        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        consumer.Received += (sender, e) =>
        {
            string message = Encoding.UTF8.GetString(e.Body.Span);
            Console.WriteLine(message);
        };

    }

    private static void SimpleExample()
    {
        Publisher();
        Consumer();
    }

    private static void Consumer()
    {
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        // queue oluşturma
        channel.QueueDeclare(queue: "example-queue", exclusive: false);

        EventingBasicConsumer consumer = new(channel);
        channel.BasicConsume(queue: "example-queue", false, consumer);

        consumer.Received += (sender, e) =>
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
        };
    }

        private static void Publisher()
    {
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://mcqcpoqg:8A8S5zibDUxxgWsQlJUuz4xHgi0atEk6@chimpanzee.rmq.cloudamqp.com/mcqcpoqg"); //bağlantı kısmı
        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();

        // queue oluşturma
        channel.QueueDeclare(queue: "example-queue", exclusive: false);

        // queue mesaj gönderme
        // rabbitmq queue atacağı mesajları byte türünde atar. mesajlar byte türünde kullanıulır.
        byte[] message = Encoding.UTF8.GetBytes("Merhaba");

        // exchange boş verirsen rabbitmq da default exchange davranışına girer ve direct exchange gibi davranır.
        channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

        Console.Read();
    }
}