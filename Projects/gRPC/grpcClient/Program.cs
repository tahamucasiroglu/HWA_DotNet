using Grpc.Net.Client;
//using grpcServiceExample;
//using grpcMessageServer;
//using grpcMessageStreamServer;
//using grpcMessageClientStream;
using grpcStreamStream;
internal class Program
{
    private static async Task Main(string[] args)
    {
        //ilk örnek
        //var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        //var greetClient = new Greeter.GreeterClient(channel);

        //HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "Selam Dünyalı" });
        //Console.WriteLine(result.Message);

        //HelloReply result2 = greetClient.SayHello(new HelloRequest { Name = "Selam Dünyalı" });
        //Console.WriteLine(result2.Message);

        // unary iletişim
        //var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        //var messageClient = new Message.MessageClient(channel);

        //MessageResponse result = await messageClient.SendMessageAsync(new MessageRequest { Name = "taha", Message="Naber dünyalı" });
        //Console.WriteLine(result.Message);

        //MessageResponse result2 = messageClient.SendMessage(new MessageRequest { Name = "taha", Message = "Naber dünyalı" });
        //Console.WriteLine(result2.Message);


        // server stream iletişim
        //var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        //var messageServerClient = new MessageStream.MessageStreamClient(channel);

        //var response = messageServerClient.SendMessage(new MessageStreamRequest { Name = "taha", Message = "Naber dünyalı" });
        //while(true)
        //{
        //    if(await response.ResponseStream.MoveNext(new CancellationToken(false)))
        //    {
        //        Console.WriteLine(response.ResponseStream.Current.Message);
        //    }

        //}


        // client stream iletişimi
        //var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        //var messageClient = new MessageClientStream.MessageClientStreamClient(channel);
        //var request = messageClient.SendMessage();
        //for(int i=0; i<10; i++)
        //{
        //    await Task.Delay(1000);
        //    await request.RequestStream.WriteAsync(new MessageClientStreamRequest
        //    {
        //        Name="Taha",
        //        Message = "Mesaj" + i.ToString()
        //    });
        //}
        //await request.RequestStream.CompleteAsync();
        //Console.WriteLine((await request.ResponseAsync).Message);



        // stream stream bi-directional

        var channel = GrpcChannel.ForAddress("http://localhost:5288");// server adresi çalıştırırsan konsolda adres verir 
        var messageClient = new MessageStreamStream.MessageStreamStreamClient(channel);

        var request = messageClient.SendMessage();
        var task1 = Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++) 
            {
                await Task.Delay(1000);
                await request.RequestStream.WriteAsync(new MessageStreamStreamRequest { Name = "Taha", Message = "mesaj" + i.ToString() });

            }
        });

        while(await request.ResponseStream.MoveNext(new CancellationTokenSource().Token))
        {
            Console.WriteLine(request.ResponseStream.Current.Message);
        }

        await task1;
        await request.RequestStream.CompleteAsync();


    }
}