using ITHS.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Answers;

/// <summary>
/// A base class for all Answer requests and responses
/// </summary>
public abstract class AnswerBase : IAnswer {
    [MaxLength(31), Required]
    public string Description { get; set; } = "";
}
