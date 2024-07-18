using Grpc.Net.Client;
using grpcServiceExample;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        var greetClient = new Greeter.GreeterClient(channel);

        HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "Selam Dünyalı" });
        Console.WriteLine(result.Message);

        HelloReply result2 = greetClient.SayHello(new HelloRequest { Name = "Selam Dünyalı" });
        Console.WriteLine(result2.Message);

    }
}