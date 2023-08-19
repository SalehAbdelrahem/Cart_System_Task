using Data;
using Final_Project;
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
        [HttpPost]

        public ResultViewModel AddProduct(InsertProductViewModel model)
        {
            result.Message = "Add Product";
            var product = model.ToProductModel();
            var category = CategoryRepo.GetByID(model.CategoryId);
            if (category is not null)
            {
                product.Category = category;
            }
            else
            {
                result.Message = "not Falid Add category For This Product";

                return result;
            }
            ProductRepo.Add(product);

            UnitOfWork.Save();

            return result;

        }
        [HttpDelete]

        public ResultViewModel DeleteProduct(int id)
        {
            try
            {
                var product = ProductRepo.GetByID(id);
                if (product is  null)
                {
                    result.Message = "Not Foun This Product ";
                    return result;
                }
                else
                {
                    ProductRepo.Remove(product);
                    UnitOfWork.Save();
                    result.Data = product;
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Message = ex.Message
                };
            }

        }
        [HttpPut]


        public ResultViewModel UpdateProduct(ProductViewModel model)
        {
            try
            {
                var product = ProductRepo.GetByID(model.Id);

                if (product is  null)
                {
                    result.Message = "Not Foun This Product ";
                    return result;
                }
                else
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Rate = model.Rate;
                    product.Description = model.Description;

                    var category = CategoryRepo.GetByID(model.CategoryId);
                    product.Category = category;

                    ProductRepo.Update(product);
                    UnitOfWork.Save();
                    result.Data = product;
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Message = ex.Message
                };
            }

        }


    }
}
