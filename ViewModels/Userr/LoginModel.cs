using System.ComponentModel.DataAnnotations;

namespace ViewModels.Userr
{
    public class LoginModel
    {
        [Required]
        public string Email { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
