using FluentValidation;
using MediatR;
using ServiceDesk.Application.Common.Models;

namespace ServiceDesk.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .Select(f => f.ErrorMessage)
            .ToList();

        if (failures.Any())
        {
            var responseType = typeof(TResponse);

            var failureMethod = responseType.GetMethods()
                .FirstOrDefault(m => m.Name == "Failure" &&
                                     m.GetParameters().Length == 1 &&
                                     m.GetParameters()[0].ParameterType == typeof(IEnumerable<string>));

            if (failureMethod != null)
            {
                var result = failureMethod.Invoke(null, [failures]);
                return (TResponse)result!;
            }
        }

        return await next(cancellationToken);
    }
}