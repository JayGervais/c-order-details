using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrderData
{
    public class OrderDB
    {
        public void GetAllOrders(ListBox listBox)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string selectOrdersQuery = @"SELECT Orders.OrderID, CustomerID, OrderDate, RequiredDate, ShippedDate, (UnitPrice * (1-Discount) * Quantity) as Total " +
                                           "FROM Orders inner join [Order Details] " +
                                            "ON Orders.OrderID = [Order Details].OrderID";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectOrdersQuery, con);
                using (sqlDataAdapter)
                {
                    DataTable AllOrdersTable = new DataTable();
                    sqlDataAdapter.Fill(AllOrdersTable);
                    listBox.SelectedValuePath = "OrderID";
                    listBox.ItemsSource = AllOrdersTable.DefaultView;
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void ShowSelectedOrder(ListBox listbox, TextBox orderID, TextBox customerID, TextBox orderDate, TextBox requiredDate, TextBox shippedDate, TextBox Total)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string selectOrderQuery = @"SELECT OrderID, CustomerID, OrderDate, RequiredDate, ShippedDate " +
                                           "FROM Orders " +
                                           "WHERE OrderID = @OrderID";

                SqlCommand sqlCommand = new SqlCommand(selectOrderQuery, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                using (sqlDataAdapter)
                {
                    sqlCommand.Parameters.AddWithValue("@OrderID", listbox.SelectedValue);
                    DataTable OrderDataTable = new DataTable();

                    sqlDataAdapter.Fill(OrderDataTable);

                    DateTime OrderDate = Convert.ToDateTime(OrderDataTable.Rows[0]["OrderDate"]);
                    DateTime RequiredDate = Convert.ToDateTime(OrderDataTable.Rows[0]["RequiredDate"]);
                    orderID.Text = OrderDataTable.Rows[0]["OrderID"].ToString();
                    customerID.Text = OrderDataTable.Rows[0]["CustomerID"].ToString();
                    orderDate.Text = OrderDate.ToString("MMM dd, yyyy");
                    requiredDate.Text = RequiredDate.ToString("MMM dd, yyyy");

                    if (shippedDate.Text == null)
                    {
                        shippedDate.Text = "";
                    }
                    else
                    {
                        DateTime ShippedDate = Convert.ToDateTime(OrderDataTable.Rows[0]["ShippedDate"]);
                        shippedDate.Text = ShippedDate.ToString("MMM dd, yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void ShowOrderDetails(ListBox listbox, TextBox productID, TextBox unitPrice, TextBox quantity, TextBox discount)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string selectOrderDetailsQuery = @"SELECT ProductID, UnitPrice, Quantity, Discount " +
                                                  "FROM [Order Details] D inner join Orders O " +
                                                  "ON D.OrderID = O.OrderID " +
                                                  "WHERE D.OrderID = @OrderID";
                SqlCommand sqlCommand = new SqlCommand(selectOrderDetailsQuery, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                using (sqlDataAdapter)
                {
                    sqlCommand.Parameters.AddWithValue("@OrderID", listbox.SelectedValue);
                    DataTable OrderDataTable = new DataTable();

                    sqlDataAdapter.Fill(OrderDataTable);

                    string Discount = Convert.ToString(OrderDataTable.Rows[0]["Discount"]);

                    productID.Text = OrderDataTable.Rows[0]["ProductID"].ToString();
                    unitPrice.Text = OrderDataTable.Rows[0]["UnitPrice"].ToString();
                    quantity.Text = OrderDataTable.Rows[0]["Quantity"].ToString();
                    discount.Text = Discount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateShippingDate(TextBox OrderID, DatePicker UpdateOrder)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string queryUpdateShipDate = @"UPDATE Orders SET ShippedDate = @ShippedDate WHERE OrderID = @OrderID";
                SqlCommand sqlCommand = new SqlCommand(queryUpdateShipDate, con);
                con.Open();
                sqlCommand.Parameters.AddWithValue("@OrderID", OrderID.Text);
                if(UpdateOrder.SelectedDate == null)
                {
                    sqlCommand.Parameters.AddWithValue("@ShippedDate", DBNull.Value);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@ShippedDate", UpdateOrder.SelectedDate);
                }
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateOrderDetails(TextBox productID, TextBox unitPrice, TextBox quantity, TextBox discount)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string query = @"UPDATE [Order Details] " +
                                "SET ProductID = @ProductID, UnitPrice = @UnitPrice, Quantity = @Quantity, Discount = @Discount " +
                                "WHERE ProductID = @ProductID";

                SqlCommand sqlCommand = new SqlCommand(query, con);
                con.Open();
                sqlCommand.Parameters.AddWithValue("@ProductID", productID.Text);
                sqlCommand.Parameters.AddWithValue("@UnitPrice", unitPrice.Text);
                sqlCommand.Parameters.AddWithValue("@Quantity", quantity.Text);
                sqlCommand.Parameters.AddWithValue("@Discount", discount.Text);
                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }

}
