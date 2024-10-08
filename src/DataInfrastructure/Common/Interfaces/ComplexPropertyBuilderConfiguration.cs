using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Common.Interfaces;

internal static class ComplexPropertyBuilderConfiguration
{
    public static ComplexPropertyBuilder<TEntity> Configure<TEntity>(
        this ComplexPropertyBuilder<TEntity> propertyBuilder,
        IComplexPropertyConfiguration<TEntity> configuration)
        where TEntity : class
    {
        configuration.Configure(propertyBuilder);
        return propertyBuilder;
    }
}