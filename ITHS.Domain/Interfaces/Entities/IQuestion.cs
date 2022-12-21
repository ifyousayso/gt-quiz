using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Interfaces.Entities;

/// <summary>
/// A base interface for all Question requests and responses
/// </summary>
public interface IQuestion {
    [MaxLength(255), Required]
    public string Description { get; set; }
}
