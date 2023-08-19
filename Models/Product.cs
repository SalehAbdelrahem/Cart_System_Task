using static System.Net.Mime.MediaTypeNames;

namespace Models
{
    public class Product : BaseModel
    {
       
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; } = 0;
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public IList<Images> Product_Images { get; set; }
        public IList<Order>? Orders { get; set; }


    }
}