using Domain.Common;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Application.Common.Mappings;

public static class MappingExtensions
{
	public static TDestination Map<TSource, TDestination>(
		this TSource source,
		IConfigurationProvider configurationProvider)
		where TSource : BaseEntity
		where TDestination : IMapToFrom<TSource> =>
		configurationProvider.CreateMapper()
			.Map<TDestination>(source);

	public static TDestination MapTo<TSource, TDestination>(
		this TSource source,
		IConfigurationProvider configurationProvider)
		where TSource : IMapToFrom<TDestination>
		where TDestination : class =>
		configurationProvider.CreateMapper()
			.Map<TDestination>(source);
	public static async Task<List<TDestination>> MapToListAsync<TSource, TDestination>(
		this IQueryable<TSource> queryable,
		IConfigurationProvider configurationProvider,
		CancellationToken cancellationToken = default)
		where TSource : BaseEntity
		where TDestination : IMapToFrom<TSource> =>
		configurationProvider
			.CreateMapper()
			.Map<List<TDestination>>(await queryable.AsNoTracking().ToListAsync(cancellationToken));

	public static List<TDestination> MapToListDataTransferObjects<TSource, TDestination>(
		this IEnumerable<TSource> source,
		IConfigurationProvider configurationProvider)
		where TSource : BaseEntity
		where TDestination : class =>
		configurationProvider
			.CreateMapper()
			.Map<List<TDestination>>(source.ToList());
}
