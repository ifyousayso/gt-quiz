using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Auth
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; } = "dawid";

        [Required]
        public string Password { get; set; } = "iths";
    }
}
