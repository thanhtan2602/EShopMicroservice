﻿using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
  public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
  {
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
      var context = new ValidationContext<TRequest>(request);

      var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

      var failrues = validationResults
        .Where(r => r.Errors.Any())
        .SelectMany(r => r.Errors)
        .ToList();

      if (failrues.Any())
      {
        throw new ValidationException(failrues);
      }

      return await next();
    }
  }
}
