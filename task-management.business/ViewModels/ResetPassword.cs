using System.ComponentModel.DataAnnotations;

namespace task_management.business.ViewModels
{
    public class ResetPassword
    {
        [Required]
        public string UserName { get; set; }
    }
}
