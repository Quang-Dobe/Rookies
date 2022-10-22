using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.SharedView.DTO.Account
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Name should not be empty")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
