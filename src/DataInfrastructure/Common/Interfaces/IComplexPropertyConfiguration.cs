using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Common.Interfaces;

internal interface IComplexPropertyConfiguration<TEntity> where TEntity : class
{
    ComplexPropertyBuilder<TEntity> Configure(ComplexPropertyBuilder<TEntity> builder);
}