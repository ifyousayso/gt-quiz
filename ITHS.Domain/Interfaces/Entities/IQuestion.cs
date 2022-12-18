using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Interfaces.Entities;

public interface IQuestion {
    [MaxLength(255), Required]
    public string Description { get; set; }
}
