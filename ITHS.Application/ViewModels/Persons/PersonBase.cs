using ITHS.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Persons
{
    /// <summary>
    /// A base class for all Person requests and responses.
    /// </summary>
    public abstract class PersonBase : IPerson
    {
        [MaxLength(30), Required]
        public string FirstName { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
