using Data;
using Models;

namespace Reposotries
{
    public class UnitOfWork : IUnitOfWork
    {
        Project_Context Context;

        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Order> OrderRepo;

        IGenericRepostory<Product> ProductRepo;

        IGenericRepostory<Images> ImagesRepo;

        public UnitOfWork(Project_Context context,
                            IGenericRepostory<Category> categoryRepo,
                            IGenericRepostory<Order> orderRepo,
                            IGenericRepostory<Product> productRepo,
                            IGenericRepostory<Images> imagesRepo
)
        {
            Context = context;
            CategoryRepo = categoryRepo;
            OrderRepo = orderRepo;
            ProductRepo = productRepo;
            ImagesRepo = imagesRepo;

        }

        public IGenericRepostory<Category> GetCategoryRepo()
        {
            return CategoryRepo;

        }

        public IGenericRepostory<Order> GetOrderRepo()
        {
            return OrderRepo;

        }

        public IGenericRepostory<Product> GetProductRepo()
        {
            return ProductRepo;

        }
        public IGenericRepostory<Images> GetImagesRepo()
        {
            return ImagesRepo;
        }


        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }


        public Project_Context context()
        {
            return Context;
        }
    }
}
