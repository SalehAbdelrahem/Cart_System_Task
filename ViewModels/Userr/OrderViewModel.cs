using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Userr
{
    public class OrderViewModel
    {
        public DateTime Order_Date { set; get; } = DateTime.Now;
        public int Quantity { set; get; }
        public string Delivery_Status { set; get; }

        public int CurrentUserID { get; set; }


    }
    public static class ProductViewModelExtensions
    {
        public static OrderViewModel OrderViewModel(this Order order)
        {

            return new OrderViewModel()
            {
                Order_Date = order.Order_Date,
                CurrentUserID = order.CurrentUserID,
                Delivery_Status = order.Delivery_Status,
                Quantity = order.Quantity,

            };
        }

    }
}