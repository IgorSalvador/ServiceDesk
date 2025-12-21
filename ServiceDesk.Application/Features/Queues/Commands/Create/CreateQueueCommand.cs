using MediatR;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Application.Features.Queues.Commands.Create;

public class CreateQueueCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}