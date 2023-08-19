using Models;

namespace Final_Project
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public IList<string> ImagesURL { get; set; }
        public int Rate { get; set; }
        public int CategoryId { get; set; }

    }
    public static class ProductViewModelExtensions
    {
        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Rate = product.Rate,
                Description = product.Description

            };
        }

    }
}
