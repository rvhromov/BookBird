using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = BookBird.Domain.Exceptions.ValidationException;

namespace BookBird.Infrastructure.PipelineBehaviors
{
    internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) 
            => _validators = validators;
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();
            
            var context = new ValidationContext<TRequest>(request);

            var errorMessages = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, messages) => new
                    {
                        Key = propertyName,
                        Values = messages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values)
                .SelectMany(x => x.Value);

            var errors = string.Join("; ", errorMessages);

            if (!string.IsNullOrWhiteSpace(errors))
                throw new ValidationException(errors);
            
            return await next();
        }
    }
}