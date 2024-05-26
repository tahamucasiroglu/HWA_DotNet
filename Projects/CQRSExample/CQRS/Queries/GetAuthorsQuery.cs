using CQRSExample.Models;
using MediatR;
using System.Collections.Generic;

namespace CQRSExample.CQRS.Queries
{
    public class GetAuthorsQuery : IRequest<List<Author>>
    {
    }

}
