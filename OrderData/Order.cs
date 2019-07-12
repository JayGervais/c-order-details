using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrderData
{
    public class Order
    {
        public Order() { }

        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }

        public Order OrderCopy()
        {
            Order copy = new Order();
            copy.OrderID = OrderID;
            copy.CustomerID = CustomerID;
            copy.OrderDate = OrderDate;
            copy.RequiredDate = RequiredDate;
            copy.ShippedDate = ShippedDate;
            return copy;
        }
    }
}
