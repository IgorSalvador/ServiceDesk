using MediatR;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Application.Features.Queues.Queries.Get;

public class GetQueuesQuery : IRequest<Result<List<QueueResponse>>>
{

}
