using System.ComponentModel.DataAnnotations;

namespace ViewModels.Userr
{
    public class ViewUser
    {
        [Required, StringLength(10)]
        public int id { set; get; }
        [Required, StringLength(200)]
        public string Full_Name { set; get; }

        [Required, StringLength(500)]

        public int Phone { set; get; }
        [Required, StringLength(300)]
        public string Email { set; get; }

    }
}
