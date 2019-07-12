using OrderData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jay_Gervais_CPRG200_Lab4
{
    /// <summary>
    /// CPRG 200 Lab Assignment 4
    /// Jay Gervais
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            OrderDB orders = new OrderDB();
            orders.GetAllOrders(lstBoxOrders);
            btnOrderDetails.IsEnabled = false;
            btnShippingDate.IsEnabled = false;
        } 

        private void ListSelectedOrders_Selection(object sender, SelectionChangedEventArgs e)
        {
            OrderDB showOrders = new OrderDB();
            showOrders.ShowSelectedOrder(lstBoxOrders, txtOrderId, txtCustomerID, txtOrderDate, txtRequiredDate, dateShippedDate, txtOrderTotal);
            showOrders.ShowOrderDetails(lstBoxOrders, txtProductID, txtUnitPrice, txtQuantity, txtDiscount);
            OrderTotal();
            btnOrderDetails.IsEnabled = true;
            btnShippingDate.IsEnabled = true;
        }

        private void BtnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            string ProductID = txtProductID.Text;
            string UnitPrice = txtUnitPrice.Text;
            int Quantity = Convert.ToInt32(txtQuantity.Text);
            double Discount = Convert.ToDouble(txtDiscount.Text);

            EditOrder editOrder = new EditOrder(ProductID, UnitPrice, Quantity, Discount);

            editOrder.Show();
            this.Close();
        }

        private void BtnShippingDate_Click(object sender, RoutedEventArgs e)
        {
            string OrderID = txtOrderId.Text;
            string ShippedDate = dateShippedDate.Text;

            EditShippingDate editShippingDate = new EditShippingDate(OrderID, ShippedDate);

            editShippingDate.Show();
            this.Close();
        }

        private void OrderTotal()
        {
            double UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
            int Quantity = Convert.ToInt32(txtQuantity.Text);
            double Discount = Convert.ToDouble(txtDiscount.Text);
            double Total = UnitPrice * (1 - Discount) * Quantity;
            txtOrderTotal.Text = Total.ToString("C");
            txtUnitPrice.Text = UnitPrice.ToString("C");
        }
    }
}
