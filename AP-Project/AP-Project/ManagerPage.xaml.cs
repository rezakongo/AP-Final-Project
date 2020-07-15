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
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace AP_Project
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        int r = 0;
        public ManagerPage()
        {
            InitializeComponent();
            SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            manager.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Manager", manager);
            DataTable table = new DataTable();
            adapter.Fill(table);
            firstname.Text = table.Rows[0][2].ToString();
            lastname.Text = table.Rows[0][3].ToString();
            regeon.Text = table.Rows[0][6].ToString();
            adress.Text = table.Rows[0][7].ToString();
            type.Text = table.Rows[0][4].ToString();
            SqlDataAdapter adapter1 = new SqlDataAdapter("select * from food", manager);
            DataTable foods = new DataTable();
            adapter1.Fill(foods);
            r = foods.Rows.Count;
            foodtable.DataContext = foods;
            manager.Close();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (System.Windows.Forms.DialogResult.OK.ToString() == "OK")
            {
                image.Text = dialog.FileName.ToString();
            }
            
            
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            manager.Open();
            string command = "update Manager set managerFirstname='" + firstname.Text.Trim() + "', managerLastname='" + lastname.Text.Trim() + "',regeon='" + regeon.Text.Trim() + "',adress='" + adress.Text.Trim()+"',type='"+type.Text.Trim()+"' where username='manager'";
            SqlCommand sqlCommand =new SqlCommand(command, manager);
            sqlCommand.ExecuteNonQuery();
            manager.Close();
            MessageBox.Show("Your Information Changed!!!");
        }

        private void Changes_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table = ((DataView)foodtable.ItemsSource).ToTable();
            for (int j = table.Rows.Count - 1; j >= 0; j--)
            {
                if (table.Rows[j][5]=="" && table.Rows[j][1] == "" && table.Rows[j][2] == ""&& table.Rows[j][3] == "" && table.Rows[j][4] == "")
                {
                    table.Rows[j].Delete();
                }
                
            }

            bool b=false;
            bool a = false;

            for (int j = 0; j < table.Rows.Count;j++)
            {
                b = false;
                for(int k = 1; k < table.Columns.Count; k++)
                {
                    if (table.Rows[j][k].ToString() == "")
                    {
                        b = true;
                        break;
                    }
                }
                
                if (!Regex.Match(table.Rows[j][3].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                {
                    a = true;
                    MessageBox.Show("Block in row : " + (j+1) + " and column : 4 should be a Number!!!");
                    break;
                }
                if (!Regex.Match(table.Rows[j][4].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                {
                    a = true;
                    MessageBox.Show("Block in row : " + (j+1) + " and column : 5 should be a Number!!!");
                    break;
                }
                if (!Regex.Match(table.Rows[j][5].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                {
                    a = true;
                    MessageBox.Show("Block in row : " + (j+1) + " and column : 6 should be a Number!!!");
                    break;
                }
                if (b)
                {
                    MessageBox.Show("Some Fields are NULL.Please try again");
                    break;
                }



                
            }
            SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            manager.Open();
            if ((!b) && (!a))
            {
                
                for (int i = 0; i < r; i++)
                {
                    string c = "update food set name='"+table.Rows[i][1].ToString()+"',caption='"+table.Rows[i][2]+"',initialPrice='"+table.Rows[i][3]+"',finalPrice='"+table.Rows[i][4]+"',inventory='"+table.Rows[i][5]+"' where num='"+(i+1)+"'";
                    
                    SqlCommand command = new SqlCommand(c, manager);
                    command.ExecuteNonQuery();
                    
                }
                for(int i = r; i < table.Rows.Count; i++)
                {
                    string c = "insert into food values('"+(i+1)+"','"+table.Rows[i][1]+"','" + table.Rows[i][2] + "','" + table.Rows[i][3] + "','" + table.Rows[i][4] + "','" + table.Rows[i][5] + "')";
                    SqlCommand command = new SqlCommand(c,manager);
                    command.ExecuteNonQuery();
                }
                
                r = table.Rows.Count;
                
            }
            SqlDataAdapter adapter1 = new SqlDataAdapter("select * from food", manager);
            DataTable foods = new DataTable();
            adapter1.Fill(foods);
            foodtable.DataContext = foods;
            manager.Close();
        }
    }
}
