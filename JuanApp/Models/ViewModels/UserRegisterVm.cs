using System.ComponentModel.DataAnnotations;

namespace JuanApp.Models.ViewModels
{
    public class UserRegisterVm
    {
        [Required]
        public string Fullname { get; set; }
        
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        [MinLength(6)]
        public string ConfirmPassword { get; set; }
        
        
    }
}
