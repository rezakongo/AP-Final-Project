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
        SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        DataTable food = new DataTable();
        public ClientPage(string email)
        {
            InitializeComponent();
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
                food.Rows[i][8] = "0";
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
    }
}
