using MediatR;
namespace CQRSExample.CQRS.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
    }
}