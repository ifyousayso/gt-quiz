using System.ComponentModel.DataAnnotations;


namespace ITHS.Domain.Interfaces.Entities
{
    public interface IPerson
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
