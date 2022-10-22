using System.ComponentModel.DataAnnotations;

namespace ECommerce.SharedView.DTO.Account
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Email should not be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name should not be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}
