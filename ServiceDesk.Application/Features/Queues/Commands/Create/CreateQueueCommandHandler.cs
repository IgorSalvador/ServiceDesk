using MediatR;
using ServiceDesk.Application.Common.Models;
using ServiceDesk.Domain.Entities;
using ServiceDesk.Infrastructure.Data;

namespace ServiceDesk.Application.Features.Queues.Commands.Create;

public class CreateQueueCommandHandler : IRequestHandler<CreateQueueCommand, Result<Guid>>
{
    private readonly AppDbContext _context;

    public CreateQueueCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateQueueCommand request, CancellationToken cancellationToken)
    {
        var entity = new ServiceQueue(request.Name, request.Description);

        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(entity.Id);
    }
}
