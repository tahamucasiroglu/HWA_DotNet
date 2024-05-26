using MediatR;
namespace CQRSExample.CQRS.Commands
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
    }
}