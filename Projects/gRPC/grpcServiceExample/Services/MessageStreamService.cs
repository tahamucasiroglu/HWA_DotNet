using Grpc.Core;
using grpcMessageStreamServer;



namespace grpcServiceExample.Services
{
    public class MessageStreamService : MessageStream.MessageStreamBase
    {
        public override async Task SendMessage(MessageStreamRequest request, IServerStreamWriter<MessageStreamResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} Name: {request.Name}");
            for (int i = 0; i < 10; i++) 
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageStreamResponse { Message = "Merhaba " + i.ToString() });
            }
        }
    }
}
