using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                string selectOrdersQuery = @"SELECT Orders.OrderID, CustomerID, OrderDate, RequiredDate, ShippedDate, SUM(UnitPrice * (1-Discount) * Quantity) as Total " +
                                            "FROM Orders inner join [Order Details] " +
                                            "ON Orders.OrderID = [Order Details].OrderID " +
                                            "GROUP BY Orders.OrderID, CustomerID, OrderDate, RequiredDate, ShippedDate";

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

        public void ShowSelectedOrder(ListBox listbox, TextBox orderID, TextBox customerID, TextBox orderDate, TextBox requiredDate, TextBox shippedDate)
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
                    orderID.Text = OrderDataTable.Rows[0]["OrderID"].ToString();
                    customerID.Text = OrderDataTable.Rows[0]["CustomerID"].ToString();
                                       
                    if (OrderDataTable.Rows[0]["OrderDate"] == DBNull.Value)
                    {
                        shippedDate.Text = "Choose a date";
                    }
                    else
                    {
                        DateTime OrderDate = Convert.ToDateTime(OrderDataTable.Rows[0]["OrderDate"]);
                        orderDate.Text = OrderDate.ToString("MMM dd, yyyy");
                    }

                    if (OrderDataTable.Rows[0]["RequiredDate"] == DBNull.Value)
                    {
                        shippedDate.Text = "Choose a date";
                    }
                    else
                    {
                        DateTime RequiredDate = Convert.ToDateTime(OrderDataTable.Rows[0]["RequiredDate"]);
                        requiredDate.Text = RequiredDate.ToString("MMM dd, yyyy");
                    }

                    if (OrderDataTable.Rows[0]["ShippedDate"] == DBNull.Value)
                    {
                        shippedDate.Text = "Choose a date";
                    }
                    else if (Convert.ToDateTime(OrderDataTable.Rows[0]["ShippedDate"]).Ticks < Convert.ToDateTime(OrderDataTable.Rows[0]["OrderDate"]).Ticks)
                    {
                        shippedDate.Text = "Shipped date must be later than order date";
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

        public void GetProductIDs(ListBox orders, ListBox products)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string selectOrdersQuery = @"SELECT OrderID, ProductID from [Order Details] WHERE OrderID = @OrderID";

                SqlCommand sqlCommand = new SqlCommand(selectOrdersQuery, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                using (sqlDataAdapter)
                {
                    sqlCommand.Parameters.AddWithValue("@OrderID", orders.SelectedValue);
                    DataTable AllProductsTable = new DataTable();
                    sqlDataAdapter.Fill(AllProductsTable);
                    products.SelectedValuePath = "ProductID";                  
                    products.ItemsSource = AllProductsTable.DefaultView;
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

        public void ShowOrderDetails(ListBox listbox, TextBox productID, TextBox unitPrice, TextBox quantity, TextBox discount, TextBox total)
        {
            SqlConnection con = NorthwindDB.GetConnection();
            try
            {
                string selectOrderDetailsQuery = @"SELECT ProductID, UnitPrice, Quantity, Discount, (UnitPrice * (1-Discount) * Quantity) as Total " +
                                                  "FROM [Order Details] " +
                                                  "WHERE ProductID = @ProductID " +
                                                  "GROUP BY ProductID, UnitPrice, Quantity, Discount";

                SqlCommand sqlCommand = new SqlCommand(selectOrderDetailsQuery, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                using (sqlDataAdapter)
                {
                    if(listbox.SelectedValue == null)
                    {
                        sqlCommand.Parameters.AddWithValue("@ProductID", DBNull.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@ProductID", listbox.SelectedValue);
                    }

                    DataTable OrderDataTable = new DataTable();

                    sqlDataAdapter.Fill(OrderDataTable);

                    if(OrderDataTable.Rows.Count > 0)
                    {
                        string Discount = Convert.ToString(OrderDataTable.Rows[0]["Discount"]);
                        productID.Text = OrderDataTable.Rows[0]["ProductID"].ToString();
                        Double UnitPrice = Convert.ToDouble(OrderDataTable.Rows[0]["UnitPrice"]);
                        unitPrice.Text = UnitPrice.ToString("C");
                        quantity.Text = OrderDataTable.Rows[0]["Quantity"].ToString();
                        discount.Text = Discount;
                        double Total = Convert.ToDouble(OrderDataTable.Rows[0]["Total"]);
                        total.Text = Total.ToString("C");
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
    }

}
