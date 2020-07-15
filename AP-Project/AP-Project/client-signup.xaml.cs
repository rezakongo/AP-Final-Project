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
    /// Interaction logic for client_signup.xaml
    /// </summary>
    public partial class client_signup : Window
    {
        public client_signup()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            client_login cl = new client_login();
            cl.Show();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection Clients = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Clients.Open();

            string firstName = clientfirstname.Text.Trim();
            string lastName = clientlastname.Text.Trim();
            string Adress = clientadress.Text.Trim();
            string phoneNumber = clientphonenumber.Text.Trim();
            string nationalID = clientid.Text.Trim();
            string eMail = clientemail.Text.Trim();
            string pass = clientpass.Text.Trim();
            string pass2 = clientpass2.Text.Trim();

            if(firstName!="" && lastName!="" && Adress!="" && phoneNumber!="" && nationalID!="" && eMail!="" && pass!="" && pass2 != "")
            {
                
                string emailExist = "select * from Clients where EMail='"+eMail+"'";
                SqlDataAdapter existedEmail = new SqlDataAdapter(emailExist, Clients);
                DataTable table = new DataTable();
                existedEmail.Fill(table);
                if (table.Rows.Count == 0)
                {
                    if(Regex.Match(eMail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase).Success)
                    {
                        if(Regex.Match(nationalID, @"^[0-9]", RegexOptions.IgnoreCase).Success)
                        {
                            if (pass == pass2)
                            {
                                if(Regex.Match(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", RegexOptions.IgnoreCase).Success)
                                {
                                    if(Regex.Match(firstName, @"^[a-zA-Z]+$", RegexOptions.IgnoreCase).Success && Regex.Match(lastName, @"^[a-zA-Z]+$", RegexOptions.IgnoreCase).Success)
                                    {
                                        string query = "insert into Clients values('"+firstName+"','"+lastName + "','" +eMail + "','" +nationalID+ "','"+phoneNumber+ "','"+Adress + "','" +pass+ "')";
                                        SqlCommand addClient = new SqlCommand(query, Clients);
                                        addClient.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invlid FirstName or LastName!!!");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Password is not Equal To Confirm Password!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid National ID!Please Try Again!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Email!Try Again!");
                    }
                }
                else
                {
                    MessageBox.Show("This E-Mail has been Taken by another People.SignUp by Another E-Mail");
                }
            }
            else
            {
                MessageBox.Show("Error!\nSomthing is NULL!!!");
            }

            Clients.Close();
        }
    }
}
