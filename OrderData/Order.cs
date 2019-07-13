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
        //public Order() { }

        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }

        public Order(int OrderID)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerID;
            this.OrderDate = OrderDate;
            this.RequiredDate = RequiredDate;
            this.ShippedDate = ShippedDate;
        }

        public override string ToString()
        {
            return this.OrderID.ToString() + "\n" + this.CustomerID + "\n" + this.OrderDate.ToString() + "\n" + this.RequiredDate.ToString();
        }
    }
}
