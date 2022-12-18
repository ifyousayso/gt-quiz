using ITHS.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Answer : BaseEntity, IAnswer {
    [MaxLength(31), Required]
    public string Description { get; set; } = "";

    public Guid QuestionId { get; set; }

    public bool IsCorrectAnswer { get; set; }
}
