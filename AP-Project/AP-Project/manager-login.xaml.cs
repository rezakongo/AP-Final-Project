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

namespace AP_Project
{
    /// <summary>
    /// Interaction logic for manager_login.xaml
    /// </summary>
    public partial class manager_login : Window
    {
        public manager_login()
        {
            InitializeComponent();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void managerlogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            manager.Open();
            string query ="select * from Manager where username='"+managerusername.Text.Trim()+"' and password='"+managerpassword.Password.Trim()+"'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, manager);
            DataTable table = new DataTable();
            manager.Close();
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                ManagerPage mp = new ManagerPage();
                mp.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid UserName or Password!!!\nPlease Try Again.");
            }
        }
    }
}
