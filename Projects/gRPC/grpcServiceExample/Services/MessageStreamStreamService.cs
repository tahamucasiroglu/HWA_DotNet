using Grpc.Core;
using grpcStreamStream;


namespace grpcServiceExample.Services
{
    public class MessageStreamStreamService : MessageStreamStream.MessageStreamStreamBase
    {
        public override async Task SendMessage(IAsyncStreamReader<MessageStreamStreamRequest> requestStream, IServerStreamWriter<MessageStreamStreamResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () =>
            {
                while(await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"message: {requestStream.Current.Message}  -  name: {requestStream.Current.Name}");
                }
            });


            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(
                    new MessageStreamStreamResponse
                    {
                        Message = "mesaj" + i.ToString()
                    });
            }

            await task1;
        }
    }
}
