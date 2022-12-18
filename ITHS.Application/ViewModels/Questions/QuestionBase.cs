using ITHS.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Questions;

/// <summary>
/// A base class for all Question requests and responses
/// </summary>
public abstract class QuestionBase : IQuestion {
    [MaxLength(255), Required]
    public string Description { get; set; } = "";
}
