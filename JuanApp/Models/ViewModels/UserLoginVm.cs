using System.ComponentModel.DataAnnotations;

namespace JuanApp.Models.ViewModels
{
    public class UserLoginVm
    {
  
        public string UsernameorEmail { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
