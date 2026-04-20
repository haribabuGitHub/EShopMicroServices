using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TReponse> :
        IRequestHandler<TQuery, TReponse> where TQuery : IQuery<TReponse> where TReponse : notnull
    {
    }
}
