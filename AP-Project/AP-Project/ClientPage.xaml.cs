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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Window
    {
        SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public ClientPage(string email)
        {
            InitializeComponent();
            manager.Open();
            string query = "select * from Clients where EMail='" + email + "'";
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, manager);
            adapter.Fill(table);
            hello.Text = "Hello "+table.Rows[0][0].ToString()+ "!!!";
            
            manager.Close();
        }
    }
}
