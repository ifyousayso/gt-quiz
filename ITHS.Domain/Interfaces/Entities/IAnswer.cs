using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Interfaces.Entities;

public interface IAnswer {
    [MaxLength(31), Required]
    public string Description { get; set; }
}
