using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using ViewModels;
using ViewModels.Orders;

namespace Task.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepository;
        IGenericRepostory<Order> OrderRepository;
        IUnitOfWork UnitOfWork;
        ResultViewModel result = new ResultViewModel();

        public OrderController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepository = unitOfWork.GetProductRepo();
            OrderRepository = UnitOfWork.GetOrderRepo();
        }

        [HttpGet]
        public ResultViewModel Index()
        {
            result.Message = "All Order";
            result.Data = UnitOfWork.context().Orders.Select(p => new
            {
                p.Id,
                p.Order_Date,
                p.Delivery_Status,
                p.Quantity,
                p.TotalPrice,
                p.User,
                p.Products
            });
            //.ToPagedList((p ?? 1), 5);
            return result;

        }
        [HttpPost]

        public ResultViewModel AddOrder(OrderDTO model)
        {
            result.Message = "Add Order";
            try
            {
                var user = UnitOfWork.context().Users.FirstOrDefault(u => u.Id == model.CurrentUserID);
                var product = ProductRepository.Get().Where(p => model.Products.Select(x => x.Id).Contains(p.Id))
                      .ToList();
                var order = new Order()
                {
                    Quantity = model.Quantity,
                    Delivery_Status = model.Delivery_Status,
                    CurrentUserID = model.CurrentUserID,
                    Order_Date = model.Order_Date,
                    TotalPrice = model.TotalPrice,
                    User = user,
                    Products = product
                };

                OrderRepository.Add(order);
                UnitOfWork.Save();
                result.Data = model;

                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }

        }
        [HttpDelete]

        public ResultViewModel DeleteOrder(int id)
        {
            try
            {
                var category = OrderRepository.GetByID(id);
                if (category is null)
                {
                    result.Message = "order not found ";
                    return result;
                }
                else
                {
                    OrderRepository.Remove(category);
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


        public ResultViewModel UpdateOrder(UpdateOrder model)
        {
            try
            {
                var order = OrderRepository.GetByID(model.Id);

                if (order is null)
                {
                    result.Message = "Order Not Found";
                    return result;
                }
                else
                {
                    order.Delivery_Status = model.Status;

                    OrderRepository.Update(order);
                    UnitOfWork.Save();
                    result.Data = model;
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
