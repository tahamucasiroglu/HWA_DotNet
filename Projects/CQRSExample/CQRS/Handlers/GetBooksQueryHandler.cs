using CQRSExample.CQRS.Queries;
using CQRSExample.Data;
using CQRSExample.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.CQRS.Handlers
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly BookShopContext _context;

        public GetBooksQueryHandler(BookShopContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books.Include(b => b.Author).ToListAsync(cancellationToken);
        }
    }
}
