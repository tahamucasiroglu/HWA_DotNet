using CQRSExample.CQRS.Commands;
using CQRSExample.Data;
using CQRSExample.Models;
using MediatR;

namespace CQRSExample.CQRS.Handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly BookShopContext _context;

        public CreateAuthorCommandHandler(BookShopContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author { Name = request.Name };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync(cancellationToken);
            return author.Id;
        }
    }
}
