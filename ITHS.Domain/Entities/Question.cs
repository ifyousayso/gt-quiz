﻿using ITHS.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Question : BaseEntity, IQuestion {
    [MaxLength(255), Required]
    public string Description { get; set; } = "";

    [Required]
    public Guid CategoryId { get; set; }

//    public Category? Category { get; set; }

    [Required]
    public Guid LanguageId { get; set; }

//    public Language? Language { get; set; }
}
