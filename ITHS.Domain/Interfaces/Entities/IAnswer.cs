using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Interfaces.Entities;

/// <summary>
/// A base interface for all Answer requests and responses
/// </summary>
public interface IAnswer {
    [MaxLength(31), Required]
    public string Description { get; set; }
}
