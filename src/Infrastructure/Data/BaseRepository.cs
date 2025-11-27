namespace AHA.CongestionTax.Infrastructure.Data
{
    using System.Threading;
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

        public async ValueTask AddAsync(TAggRoot aggregate, CancellationToken cancellationToken = default) =>
                    await _dbContext.Set<TAggRoot>().AddAsync(aggregate, cancellationToken);


        public async Task<List<TAggRoot>> FindAllAsync(CancellationToken cancellationToken = default) =>
                    await _dbContext.Set<TAggRoot>().ToListAsync(cancellationToken);

        public async ValueTask<TAggRoot?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
                    await _dbContext.Set<TAggRoot>().FindAsync([id], cancellationToken);

        public ValueTask ModifyAsync(TAggRoot aggregate, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TAggRoot>().Update(aggregate);
            return ValueTask.CompletedTask;
        }

        public async ValueTask RemoveAsync(int aggregateRootId, CancellationToken cancellationToken = default)
        {
            var entity = await FindByIdAsync(aggregateRootId, cancellationToken);
            if (entity is not null) _dbContext.Set<TAggRoot>().Remove(entity);
        }
    }
}