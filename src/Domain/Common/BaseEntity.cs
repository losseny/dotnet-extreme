using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; private set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity()
    {
    }
}
