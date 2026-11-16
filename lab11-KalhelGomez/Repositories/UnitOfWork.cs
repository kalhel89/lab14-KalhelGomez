using TuApp.Domain.Repositories;
using TuApp.Infrastructure.Context;

namespace lab11_KalhelGomez.Repositories;

public sealed class UnitOfWork(TicketeraContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}
