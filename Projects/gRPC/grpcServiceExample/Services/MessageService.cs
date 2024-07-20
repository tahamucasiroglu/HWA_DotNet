using Grpc.Core;
using grpcMessageServer;
namespace grpcServiceExample.Services
{
    public class MessageService : Message.MessageBase
    {
        public override async Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine($"mesage = {request.Message}  -  name = {request.Name}");
            
            return new MessageResponse 
            {
                Message = "Mesaj Başarıyla alındı"
            };
            
            //return base.SendMessage(request, context);
        }
    }
}
