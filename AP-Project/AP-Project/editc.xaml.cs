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
using System.Text.RegularExpressions;
using System.Data;

namespace AP_Project
{
    
    /// <summary>
    /// Interaction logic for editc.xaml
    /// </summary>
    public partial class editc : Window
    {
        string ma;
        public editc(string m)
        {
            InitializeComponent();
            ma = m;
                SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                connection.Open();
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Clients where EMail='"+m+"'",connection);
                adapter.Fill(table);
                ad.Text = table.Rows[0][5].ToString();
                fn.Text = table.Rows[0][0].ToString();
                ln.Text = table.Rows[0][1].ToString();
                ni.Text = table.Rows[0][3].ToString();
                pass.Text = table.Rows[0][6].ToString();
                connection.Close();
            
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Regex.Match(ni.Text.Trim(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                {

                    if (Regex.Match(pass.Text.Trim(), @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", RegexOptions.IgnoreCase).Success)
                    {
                        if (Regex.Match(fn.Text.Trim(), @"^[a-zA-Z]+$", RegexOptions.IgnoreCase).Success && Regex.Match(ln.Text.Trim(), @"^[a-zA-Z]+$", RegexOptions.IgnoreCase).Success)
                        {
                            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                            connection.Open();
                            string q = "update Clients set FirstName='" + fn.Text.Trim() + "', LastName='" + ln.Text.Trim() + "',NationalID='" + ni.Text.Trim() + "', Password='" + pass.Text.Trim() + "' where EMail='" + ma.ToString() + "'";
                            SqlCommand command = new SqlCommand(q, connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                            this.Close();
                            ClientPage page = new ClientPage(ma);
                            page.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invlid FirstName or LastName!!!");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Password is not OK!!!");
                    }
                }
                else
                {
                    MessageBox.Show("National ID is not OK!!!");
                }
            }
            catch
            {

            }
        }
    }
}
