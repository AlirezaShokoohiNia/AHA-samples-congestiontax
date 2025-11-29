namespace AHA.CongestionTax.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Domain;
    using AHA.CongestionTax.Domain.Abstractions;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Default EF Core implementation of <see cref="IRepository{TAggRoot}"/>.
    /// Provides basic asynchronous CRUD operations for aggregate roots.
    /// </summary>
    /// <remarks>
    /// This repository is minimal by design and intended for typical write-side operations.
    /// Query-side concerns should be implemented separately in the Application.Read layer.
    /// </remarks>
    public class BaseRepository<TAggRoot>(AppDbContext dbContext) : IRepository<TAggRoot>
        where TAggRoot : AggregateRoot
    {
        protected readonly AppDbContext _dbContext = dbContext;

        public IUnitOfWork UnitOfWork => _dbContext;

        #region Read Operations

        public async ValueTask<Result<TAggRoot>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _dbContext.Set<TAggRoot>().FindAsync([id], cancellationToken);
                return entity is null
                    ? Result.Failure<TAggRoot>($"Entity of type {typeof(TAggRoot).Name} with id {id} not found.")
                    : Result.Success(entity);
            }
            catch (Exception ex)
            {
                return Result.Failure<TAggRoot>(ex.Message);
            }
        }

        public async Task<Result<List<TAggRoot>>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _dbContext.Set<TAggRoot>().ToListAsync(cancellationToken);
                return Result.Success(list);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<TAggRoot>>(ex.Message);
            }
        }

        #endregion

        #region Write Operations

        public async ValueTask<Result> AddAsync(TAggRoot aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                _ = await _dbContext.Set<TAggRoot>().AddAsync(aggregate, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public ValueTask<Result> ModifyAsync(TAggRoot aggregate, CancellationToken cancellationToken = default)
        {
            try
            {
                _ = _dbContext.Set<TAggRoot>().Update(aggregate);
                return ValueTask.FromResult(Result.Success());
            }
            catch (Exception ex)
            {
                return ValueTask.FromResult(Result.Failure(ex.Message));
            }
        }

        public async ValueTask<Result> RemoveAsync(int aggregateRootId, CancellationToken cancellationToken = default)
        {
            try
            {
                var entityResult = await FindByIdAsync(aggregateRootId, cancellationToken);
                if (!entityResult.IsSuccess || entityResult.Value is null)
                    return Result.Failure($"Entity of type {typeof(TAggRoot).Name} with id {aggregateRootId} not found.");

                _ = _dbContext.Set<TAggRoot>().Remove(entityResult.Value);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        #endregion
    }
}
