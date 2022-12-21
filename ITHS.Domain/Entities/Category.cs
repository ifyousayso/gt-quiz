using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Category : BaseEntity {
    [MaxLength(15), Required]
    public string Name { get; set; } = "";
}
