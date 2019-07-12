using OrderData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Jay_Gervais_CPRG200_Lab4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public OrderDetail orderDetail;

        public EditOrder(string ProductID, double UnitPrice, int Quantity, double Discount)
        {
            InitializeComponent();
            txtProductID.Text = ProductID;
            txtUnitPrice.Text = UnitPrice.ToString("C");
            txtQuantity.Text = Quantity.ToString();
            txtDiscount.Text = Discount.ToString();
        }

        private void BtnCancelUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }

        private void BtnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            TextBox productID = txtProductID;
            TextBox unitPrice = txtUnitPrice;
            TextBox quantity = txtQuantity;
            TextBox discount = txtDiscount;

            OrderDB updateOrdersDetails = new OrderDB();
            updateOrdersDetails.UpdateOrderDetails(productID, unitPrice, quantity, discount);

            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close(); 
        }
    }
}
