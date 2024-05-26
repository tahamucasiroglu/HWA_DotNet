using CQRSExample.CQRS.Commands;
using CQRSExample.Data;
using CQRSExample.Models;
using MediatR;

namespace CQRSExample.CQRS.Handlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly BookShopContext _context;

        public CreateBookCommandHandler(BookShopContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book { Title = request.Title, AuthorId = request.AuthorId };
            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);
            return book.Id;
        }
    }
}
