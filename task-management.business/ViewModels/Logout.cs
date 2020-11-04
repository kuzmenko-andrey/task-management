using System.ComponentModel.DataAnnotations;

namespace task_management.business.ViewModels
{
    public class Logout
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
