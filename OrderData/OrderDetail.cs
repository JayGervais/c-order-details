using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrderData
{
    public class OrderDetail
    {
        public OrderDetail() { }

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        public OrderDetail OrderDetailCopy()
        {
            OrderDetail copy = new OrderDetail();
            copy.ProductID = ProductID;
            copy.UnitPrice = UnitPrice;
            copy.Quantity = Quantity;
            copy.Discount = Discount;
            return copy;
        }

    }
}
