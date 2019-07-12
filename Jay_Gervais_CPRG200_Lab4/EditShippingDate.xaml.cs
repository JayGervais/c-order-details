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
using System.Windows.Shapes;

namespace Jay_Gervais_CPRG200_Lab4
{
    /// <summary>
    /// Interaction logic for EditShippingDate.xaml
    /// </summary>
    public partial class EditShippingDate : Window
    {
        public EditShippingDate(string OrderID, string ShippedDate)
        {
            InitializeComponent();

            txtOrderID.Text = OrderID;
            if (ShippedDate == null)
            {
                ShippedDatePicker.SelectedDate = null;
            }
            else if(ShippedDatePicker.SelectedDate == null)
            {
                ShippedDatePicker.SelectedDate = null;
            }
            else
            {
                ShippedDatePicker.SelectedDate = Convert.ToDateTime(ShippedDate);
            }
        }

        private void BtnCancelUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }

        private void UpdateShipping_Click(object sender, RoutedEventArgs e)
        {
            TextBox OrderID = txtOrderID;
            DatePicker UpdateOrder = ShippedDatePicker;

            OrderDB updateShippingDate = new OrderDB();
            updateShippingDate.UpdateShippingDate(OrderID, UpdateOrder);

            MainWindow mainWin = new MainWindow();
            mainWin.Show();
            this.Close();
        }
    }
}
