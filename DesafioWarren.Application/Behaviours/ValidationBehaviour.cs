using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Application.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DesafioWarren.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .ToList();

            return validationFailures.Any() ? CreateErrorResponse(validationFailures) : await next();
        }

        private static TResponse CreateErrorResponse(IEnumerable<ValidationFailure> validationFailures)
        {
            var response = new Response();

            response.AddValidationFailures(validationFailures);

            return response as TResponse;
        }
    }

}