using Grpc.Core;
using grpcMessageClientStream;

namespace grpcServiceExample.Services
{
    public class MessageClientStreamService : MessageClientStream.MessageClientStreamBase
    {
        public override async Task<MessageClientStreamResponse> SendMessage(IAsyncStreamReader<MessageClientStreamRequest> requestStream, ServerCallContext context)
        {
            while(await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"message: {requestStream.Current.Message}  | name = {requestStream.Current.Name}");
            }

            return new MessageClientStreamResponse{ Message= "Verilkeriniz alınıdı" };
        }

    }
}
