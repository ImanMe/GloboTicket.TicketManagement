using System.Collections.Generic;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWIthEventsQuery : IRequest<List<CategoryEventListVm>>, IRequest<CategoryEventListVm>
    {
        public bool IncludeHistory { get; set; }
    }
}
