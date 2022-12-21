using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Language : BaseEntity {
    [MaxLength(15), Required]
    public string Name { get; set; } = "";
}
