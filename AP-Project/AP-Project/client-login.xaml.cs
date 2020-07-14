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
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace AP_Project
{
    /// <summary>
    /// Interaction logic for client_login.xaml
    /// </summary>
    public partial class client_login : Window
    {
        

        public client_login()
        {
            InitializeComponent();
        }

        private void cback(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void signup(object sender, RoutedEventArgs e)
        {
            this.Hide();
            client_signup cs = new client_signup();
            cs.Show();
        }

        private void clientlogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection clientsAccess = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            clientsAccess.Open();
            string query;
            if (Regex.Match(clientrusername.Text.Trim(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
            {
                query = "select * from Clients where PhoneNumber='" +clientrusername.Text.Trim() + "' and Password='" + clientpassword.Text.Trim() + "'";
            }
            else
            {
                query = "select * from Clients where EMail='" + clientrusername.Text.Trim() + "' and Password='" + clientpassword.Text.Trim() + "'";
            }
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query,clientsAccess);

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                OrderPage orderPage = new OrderPage(dataTable.Rows[0][0].ToString(),dataTable.Rows[0][2].ToString());
                
                
                this.Close();
                orderPage.Show();
               
            }
            else
            {
                MessageBox.Show("Incorrect UserName or Password!!!");
            }
        }
    }
}
