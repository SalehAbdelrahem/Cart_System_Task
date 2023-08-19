using Data;
using Final_Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using ViewModels;
using ViewModels.Categories;

namespace Task.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Product> ProductRepo;
        IUnitOfWork UnitOfWork;
        ResultViewModel result = new ResultViewModel();
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo=unitOfWork.GetProductRepo();
            CategoryRepo = UnitOfWork.GetCategoryRepo();
        }
        [HttpGet]
        public ResultViewModel Index()
        {
            result.Message = "All Categories";
            result.Data = UnitOfWork.context().Categories.Select(p => new
            {
                p.Id,
                p.Name,
                p.Products
            });
            //.ToPagedList((p ?? 1), 5);
            return result;

        }
        [HttpPost]

        public ResultViewModel AddCategory(CategoryDTO model)
        {
            result.Message = "Add Category";
            try
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };

                CategoryRepo.Add(category);
                UnitOfWork.Save();
                result.Data = category;

                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

        }
        [HttpDelete]

        public ResultViewModel DeleteCategory(int id)
        {
            try
            {
                var category = CategoryRepo.GetByID(id);
                if (category is  null)
                {
                    result.Message = "Not Found This category ";
                    return result;
                }
                else
                {
                    CategoryRepo.Remove(category);
                    UnitOfWork.Save();
                    result.Data = category;
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


        public  ResultViewModel UpdateProduct(UpdateCategoryDTO model)
        {
            try
            {
                var category = CategoryRepo.GetByID(model.Id);

                if (category is  null)
                {
                    result.Message = "Not Found This Category ";
                    return result;
                }
                else
                {
                    category.Name = model.Name;
                    category.Description = model.Description;
                    CategoryRepo.Update(category);
                    UnitOfWork.Save();
                    result.Data = category;
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
