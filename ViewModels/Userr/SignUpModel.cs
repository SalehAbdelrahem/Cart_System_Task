using Models;
using System.ComponentModel.DataAnnotations;
namespace ViewModels.Userr
{
    public class SignUpModel
    {
        [Required, StringLength(200)]
        public string Full_Name { set; get; }
        public string PhoneNumber { set; get; }
        [Required, StringLength(300)]
        public string Email { set; get; }
        [Required, StringLength(300)]
        public string Password { set; get; }

        public string? ImageUrl { set; get; }


    }
    public static class SignUpModelExtensions
    {
        public static User ToModel(this SignUpModel signUpModel)
        {
            return new User
            {
                Name = signUpModel.Full_Name,
                UserName = signUpModel.Full_Name,
                PhoneNumber = signUpModel.PhoneNumber,
                Email = signUpModel.Email
            };
        }
    }
}
