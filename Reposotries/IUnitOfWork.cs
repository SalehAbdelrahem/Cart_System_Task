using Data;
using Models;

namespace Reposotries
{
    public interface IUnitOfWork
    {

        IGenericRepostory<Category> GetCategoryRepo();

        IGenericRepostory<Order> GetOrderRepo();

        IGenericRepostory<Product> GetProductRepo();

        IGenericRepostory<Images> GetImagesRepo();

        Project_Context context();

        int Save();

    }
}
