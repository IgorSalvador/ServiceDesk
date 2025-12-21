using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Application.Common.Models;
using ServiceDesk.Infrastructure.Data;

namespace ServiceDesk.Application.Features.Queues.Queries.Get;

public class GetQueuesQueryHandler : IRequestHandler<GetQueuesQuery, Result<List<QueueResponse>>>
{
    private readonly AppDbContext _context;

    public GetQueuesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<QueueResponse>>> Handle(GetQueuesQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.ServiceQueues
            .AsNoTracking()
            .Select(x => new QueueResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);

        return Result<List<QueueResponse>>.Success(list);
    }
}
