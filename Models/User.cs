using Microsoft.AspNetCore.Identity;
namespace Models
{
    public class User: IdentityUser<int>
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Images? User_Image { get; set; }


    }
}
