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
    /// 
    /// CPRG 200 Lab Assignment 4
    /// View and Edit Order Details
    /// Author: Jay Gervais
    /// Date: July 16, 2019
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            // create new OrderDB object to show all orders
            OrderDB orders = new OrderDB();
            orders.GetAllOrders(lstBoxOrders);
            // enable buttons based on user interaction
            btnOrderDetails.IsEnabled = false;
            btnShippingDate.IsEnabled = false;
            lstBoxProductIDs.IsEnabled = false;
        } 

        private void ListSelectedOrders_Selection(object sender, SelectionChangedEventArgs e)
        {
            lstBoxProductIDs.ItemsSource = null;
            lstBoxProductIDs.Items.Clear();
            txtOrderId.Clear();
            txtCustomerID.Clear();
            txtOrderDate.Clear();
            txtRequiredDate.Clear();
            dateShippedDate.Clear();
            txtProductID.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtDiscount.Clear();
            txtOrderTotal.Clear();

            OrderDB showOrders = new OrderDB();
            // show data from orders when selected in listbox
            showOrders.ShowSelectedOrder(lstBoxOrders, txtOrderId, txtCustomerID, txtOrderDate, txtRequiredDate, dateShippedDate);
            // enable buttons
            btnOrderDetails.IsEnabled = true;
            btnShippingDate.IsEnabled = true;
            lstBoxProductIDs.IsEnabled = true;
            showOrders.GetProductIDs(lstBoxOrders, lstBoxProductIDs);
        }

        private void BtnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            // create variables from text fields to pass
            string ProductID = txtProductID.Text;
            string UnitPrice = txtUnitPrice.Text;
            int Quantity = Convert.ToInt32(txtQuantity.Text);
            double Discount = Convert.ToDouble(txtDiscount.Text);
            // pass variables to EditOrder function
            EditOrder editOrder = new EditOrder(ProductID, UnitPrice, Quantity, Discount);
            // show edit window on click
            editOrder.Show();
            this.Close();
        }

        private void BtnShippingDate_Click(object sender, RoutedEventArgs e)
        {
            // create variables
            string OrderID = txtOrderId.Text;
            string ShippedDate = dateShippedDate.Text;
            string OrderDate = txtOrderDate.Text;
            // pass variables to EditShippingDate function
            EditShippingDate editShippingDate = new EditShippingDate(OrderID, ShippedDate, OrderDate);
            // show edit window
            editShippingDate.Show();
            this.Close();
        }

        private void LstBoxProductIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderDB showOrderDetails = new OrderDB();
            showOrderDetails.ShowOrderDetails(lstBoxProductIDs, txtProductID, txtUnitPrice, txtQuantity, txtDiscount, txtOrderTotal);
        }

    }
}
