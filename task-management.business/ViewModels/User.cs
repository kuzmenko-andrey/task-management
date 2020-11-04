using System.ComponentModel.DataAnnotations;

namespace task_management.business.ViewModels
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
