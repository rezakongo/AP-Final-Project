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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AP_Project
{

    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Window
    {
        DataTable ords = new DataTable();
        DataTable currentorder = new DataTable();
        SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        DataTable food = new DataTable();
        string em;
        
        public ClientPage(string email)
        {
            InitializeComponent();
            em = email;
            currentorder.Columns.Add("name");
            currentorder.Columns.Add("number");
            
            currentorder.Columns.Add("first Price");
            currentorder.Columns.Add("Final Price");
            currentorder.Columns.Add("date");
            cdg.DataContext = currentorder;
            manager.Open();
            string query = "select * from Clients where EMail='" + email + "'";
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, manager);
            adapter.Fill(table);
            hello.Text = "Hello "+table.Rows[0][0].ToString()+ "!!!";
            
            DataTable allfood = new DataTable();
            query = "select * from food";
            

            adapter = new SqlDataAdapter(query, manager);
            adapter.Fill(food);
            foodgrid.DataContext = food;
            food.Columns.Add("Number");
            for(int i = 0; i < food.Rows.Count; i++)
            {
                food.Rows[i][9] = "0";
            }
            try
            {
                query = "select * from ords where mail='" + em.ToString() + "'";
                adapter = new SqlDataAdapter(query, manager);
                adapter.Fill(ords);
                allorder.DataContext = ords;
            }
            catch
            {

            }
            manager.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table = ((DataView)foodgrid.ItemsSource).ToTable();
            for (int j = table.Rows.Count - 1; j >= 0; j--)
            {
                if (table.Rows[j][2] == "")
                {
                    table.Rows[j][2] = "0";
                }
                if (!Regex.Match(table.Rows[j][2].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                {
                    table.Rows[j][2] = "0";
                }
                

            }
            foodgrid.DataContext = table;
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                manager.Open();
                DataTable food = new DataTable();
                DataTable allfood = new DataTable();
                string query; query = "select * from food where type='" + ty.SelectedItem.ToString() + "'";
                if (ty.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    query = "select * from food where day='" + cal.SelectedDate.ToString() + "'";
                }
                else
                {
                    query = "select * from food where type='" + ty.SelectedItem.ToString() + "' and day='" + cal.SelectedDate.ToString() + "'";
                }
                Console.WriteLine(ty.SelectedItem.ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(query, manager);
                adapter.Fill(food);
                foodgrid.DataContext = food;
                food.Columns.Add("Number");
                for (int i = 0; i < food.Rows.Count; i++)
                {
                    food.Rows[i][2] = "0";
                }
                manager.Close();
            }
            catch
            {

            }
        }

        private void foodgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string r = foodgrid.SelectedIndex.ToString();
                DataTable dt = new DataTable();
                dt = ((DataView)foodgrid.ItemsSource).ToTable();
                foodim.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(dt.Rows[int.Parse(r)][6]);
            }
            catch
            {

            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int price = 0;
                int fprice = 0;
                string r = foodgrid.SelectedIndex.ToString();
                DataTable dt = new DataTable();
                dt = ((DataView)foodgrid.ItemsSource).ToTable();
                currentorder.Rows.Add();
                currentorder.Rows[currentorder.Rows.Count - 1][0] = dt.Rows[int.Parse(r)][1];
                currentorder.Rows[currentorder.Rows.Count - 1][1] = dt.Rows[int.Parse(r)][9];
                currentorder.Rows[currentorder.Rows.Count - 1][2] = dt.Rows[int.Parse(r)][3];
                currentorder.Rows[currentorder.Rows.Count - 1][3] = dt.Rows[int.Parse(r)][4];
                currentorder.Rows[currentorder.Rows.Count - 1][4] = dt.Rows[int.Parse(r)][7];
                cdg.DataContext = currentorder;
                for(int i = 0; i < currentorder.Rows.Count; i++)
                {
                    price += (int.Parse(currentorder.Rows[i][3].ToString()))*(int.Parse(currentorder.Rows[i][1].ToString()));
                    fprice += int.Parse(currentorder.Rows[i][2].ToString()) * (int.Parse(currentorder.Rows[i][1].ToString()));
                }
                finalprice.Text = price.ToString();
                firstprice.Text = fprice.ToString();
            }
            catch
            {

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ref_Click(object sender, RoutedEventArgs e)
        {
            
            int price = 0;
            int fprice = 0;
            for (int i = 0; i < currentorder.Rows.Count; i++)
            {
                price += (int.Parse(currentorder.Rows[i][3].ToString())) * (int.Parse(currentorder.Rows[i][1].ToString()));
                fprice += int.Parse(currentorder.Rows[i][2].ToString()) * (int.Parse(currentorder.Rows[i][1].ToString()));
            }
            finalprice.Text = price.ToString();
            firstprice.Text = fprice.ToString();
        }

        private void saveorder_Click(object sender, RoutedEventArgs e)
        {

            int price = 0;
            int fprice = 0;
            for (int i = 0; i < currentorder.Rows.Count; i++)
            {
                price += (int.Parse(currentorder.Rows[i][3].ToString())) * (int.Parse(currentorder.Rows[i][1].ToString()));
                fprice += int.Parse(currentorder.Rows[i][2].ToString()) * (int.Parse(currentorder.Rows[i][1].ToString()));
            }
            finalprice.Text = price.ToString();
            firstprice.Text = fprice.ToString();

            string ord = "";

                DataTable table = new DataTable();
                table = ((DataView)cdg.ItemsSource).ToTable();
                ord += table.Rows[0][0] + "-" + table.Rows[0][1] + "-" + table.Rows[0][4];
                for (int i = 1; i < table.Rows.Count; i++)
                {
                    ord += "," + table.Rows[i][0] + "-" + table.Rows[i][1] + "-" + table.Rows[i][4];
                }
                SqlConnection orders = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                orders.Open();
                string query = "select * from Clients where EMail='" + em.ToString() + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, orders);
                DataTable table1 = new DataTable();
                adapter.Fill(table1);
                string adress = table1.Rows[0][5].ToString();
                query = "select * from c";
                adapter = new SqlDataAdapter(query, orders);
                DataTable table2 = new DataTable();
                adapter.Fill(table2);
                string code = (int.Parse(table2.Rows[table2.Rows.Count - 1][0].ToString())+1).ToString();
                query = "insert into c values ('" + (int.Parse(code)).ToString() + "')";
                SqlCommand command = new SqlCommand(query, orders);
                command.ExecuteNonQuery();
                query = "insert into ords values ('" + code.ToString() + "','" + em.ToString() + "','" + adress.ToString() + "','" + ord.ToString() + "','" + firstprice.Text.ToString() + "','" + finalprice.Text.ToString() + "','no')";
                command = new SqlCommand(query, orders);
                command.ExecuteNonQuery();
                query = "select * from ords where mail='" + em.ToString() + "'";
                adapter = new SqlDataAdapter(query, orders);
            ords = new DataTable();
                adapter.Fill(ords);
                allorder.DataContext = ords;
                orders.Close();
                MessageBox.Show("Your Order Saves !!!");
                currentorder = new DataTable();
                currentorder.Columns.Add("name");
                currentorder.Columns.Add("number");
                currentorder.Columns.Add("first Price");
                currentorder.Columns.Add("Final Price");
                currentorder.Columns.Add("date");
                cdg.DataContext = currentorder;
            
            
        }

        private void pay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                data = ((DataView)allorder.ItemsSource).ToTable();
                string r = allorder.SelectedIndex.ToString();
                string cod = data.Rows[int.Parse(r)][0].ToString();
                if (data.Rows[int.Parse(r)][6].ToString() == "no")
                {
                    SqlConnection orders = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    orders.Open();
                    string query = "update ords set pay='yes' where code='" + cod + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, orders);
                    sqlCommand.ExecuteNonQuery();
                    query = "select * from ords";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, orders);
                    data = new DataTable();
                    dataAdapter.Fill(data);

                    allorder.DataContext = data;
                    orders.Close();
                    MessageBox.Show("Payed Successfully!!!");
                }
                else
                {
                    MessageBox.Show("You Paid Before!!!");
                }
            }
            catch { }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable data = new DataTable();
                data = ((DataView)allorder.ItemsSource).ToTable();
                string r = allorder.SelectedIndex.ToString();
                string cod = data.Rows[int.Parse(r)][0].ToString();
                if (data.Rows[int.Parse(r)][6].ToString() == "no")
                {
                    SqlConnection orders = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    orders.Open();
                    string query = "delete ords  where code='" + cod + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, orders);
                    sqlCommand.ExecuteNonQuery();
                    query = "select * from ords";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, orders);
                    data = new DataTable();
                    dataAdapter.Fill(data);

                    allorder.DataContext = data;
                    orders.Close();
                    MessageBox.Show("Canceled Successfully!!!");
                }
                else
                {
                    MessageBox.Show("You Paid Before!!!");
                }
            }
            catch { }
        }
    }
}
