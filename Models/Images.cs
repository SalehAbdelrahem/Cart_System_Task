using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Images : BaseModel
    {
        public string Image_URL { set; get; }
        public Product? product { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}
