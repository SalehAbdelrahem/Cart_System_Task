using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using ViewModels;

namespace Task.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Images> ImageRepo;
        IUnitOfWork UnitOfWork;
        IUserRepository UserRepository;

        Project_Context Context;
        ResultViewModel result = new ResultViewModel();

        public ProductsController(Project_Context context, 
                                 IUserRepository userRepository,
                                 IUnitOfWork unitOfWork)
        {
            Context = context;
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
           
            CategoryRepo = UnitOfWork.GetCategoryRepo();
            ImageRepo = UnitOfWork.GetImagesRepo();
            UserRepository = userRepository;
        }
        [HttpGet]
        public ResultViewModel Index()
        {
            result.Message = "All Products";
            result.Data = UnitOfWork.context().Products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Category,
                p.Description,
                p.Rate,
                p.Product_Images,
                p.Quantity,
                p.Orders,
            });
            //.ToPagedList((p ?? 1), 5);
            return result;

        }

    }
}
