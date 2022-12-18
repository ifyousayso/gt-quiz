using ITHS.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Domain.Entities;

public class Category : BaseEntity {
    [MaxLength(15), Required]
    public CategoryName CategoryName { get; set; }

    public Category() {}

    public Category(Guid id, CategoryName categoryName) { 
        Id = id;
        CategoryName = categoryName; 
    }
}
