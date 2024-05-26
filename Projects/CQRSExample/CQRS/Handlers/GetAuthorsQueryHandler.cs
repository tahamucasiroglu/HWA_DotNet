using CQRSExample.CQRS.Queries;
using CQRSExample.Data;
using CQRSExample.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.CQRS.Handlers
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
    {
        private readonly BookShopContext _context;

        public GetAuthorsQueryHandler(BookShopContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync(cancellationToken);
        }
    }
}
