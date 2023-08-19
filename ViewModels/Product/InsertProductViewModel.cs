using Models;

namespace ViewModels
{
    public class InsertProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rate { get; set; }
       
        public int Quantity { get; set; }

        public string[] Imgespathes { get; set; }
    }
    public static class InsertProductViewModelExtensions
    {
        public static InsertProductViewModel ToInsertViewModel(this Product product)
        {
            return new InsertProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                // Image = product.Image,
                Rate = product.Rate,
                Description = product.Description,
               
            };
        }

    }
}
