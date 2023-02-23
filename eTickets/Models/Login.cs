using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Login
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "User Name is required")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }
        }
    }
}
