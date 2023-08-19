using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Orders
{
    public class OrderDTO
    {
        public DateTime Order_Date { set; get; } = DateTime.Now;
        public int Quantity { set; get; }
        public string Delivery_Status { set; get; } = "Pending";
        public double TotalPrice { get; set; }
        public int CurrentUserID { get; set; }
        public IList<InsertProductViewModel> Products { get; set; }
    }
}
