using System;
using System.Threading;
using System.Threading.Tasks;
using DesafioWarren.Infrastructure.EntityFramework.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesafioWarren.Application.Behaviours
{
    public class TransactionalBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly AccountsDbContext _context;

        private readonly ILogger<TransactionalBehaviour<TRequest, TResponse>> _logger;

        public TransactionalBehaviour(AccountsDbContext context, ILogger<TransactionalBehaviour<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var response = default(TResponse);

                if (_context.HasActiveTransaction) return await next();

                _logger.LogInformation("Creating execution strategy for database.");

                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    await using var transaction = await _context.BeginTransactionAsync(cancellationToken);

                    _logger.LogInformation("Database transaction started - transaction id: {TransactionId}."
                        , _context.CurrentTransaction.TransactionId);

                    response = await next();

                    await _context.CommitTransactionAsync(transaction, cancellationToken);

                    _logger.LogInformation("Database transaction committed.");
                });

                return response;
            }
            catch (Exception exception)
            {

                _logger.LogError(exception, "An exception occurred when executing the transactional behaviour.");
                
                return default;
            }
        }
    }
}