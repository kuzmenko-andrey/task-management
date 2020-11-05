using System.ComponentModel.DataAnnotations;

namespace task_management.business.ViewModels
{
    public class SignUp
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
