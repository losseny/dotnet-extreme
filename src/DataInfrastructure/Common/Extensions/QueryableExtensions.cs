using Domain.Common;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Data.Common.Extensions;

public static class QueryableExtensions
{
	public static async Task<TEntity> FindEntityAsync<TEntity>
		(this IQueryable<TEntity> queryable, Guid id, CancellationToken cancellationToken = default)
		where TEntity : BaseEntity =>
		await queryable.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken)
		?? throw new NotFoundException(typeof(TEntity).Name, id.ToString());
}
