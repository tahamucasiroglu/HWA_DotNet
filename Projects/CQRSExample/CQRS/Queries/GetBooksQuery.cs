using CQRSExample.Models;
using MediatR;

namespace CQRSExample.CQRS.Queries
{
    public class GetBooksQuery : IRequest<List<Book>>
    {
    }
}
