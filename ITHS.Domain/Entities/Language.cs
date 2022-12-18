using ITHS.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Language : BaseEntity {
    [MaxLength(15), Required]
    public LanguageName LanguageName { get; set; }

    public Language() {}

    public Language(Guid Id, LanguageName LanguageName) { 
        this.Id = Id;
        this.LanguageName = LanguageName; 
    }
}
