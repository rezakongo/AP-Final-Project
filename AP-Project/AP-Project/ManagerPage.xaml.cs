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
using System.Security.Cryptography;

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
            int inc= 0;
            int outc = 0;
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
            
            string query = "select * from ords";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, manager);
            DataTable data = new DataTable();
            dataAdapter.Fill(data);

            allorders.DataContext = data;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                outc +=int.Parse(data.Rows[i][4].ToString());
                inc += int.Parse(data.Rows[i][5].ToString());
            }

            outcome.Text = outc.ToString();
            income.Text = inc.ToString();
            pro.Text = (inc - outc).ToString();
                manager.Close();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (System.Windows.Forms.DialogResult.OK.ToString() == "OK")
            {
                image.Text = dialog.FileName.ToString();
                im.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(image.Text.Trim());
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
            try
            {
                DataTable table = new DataTable();
                table = ((DataView)foodtable.ItemsSource).ToTable();
                string r = foodtable.SelectedIndex.ToString();
                table.Rows[int.Parse(r)][7] = cal.SelectedDate.ToString();
                table.Rows[int.Parse(r)][6] = it.Text.Trim();
                table.Rows[int.Parse(r)][8] = t.SelectedItem.ToString();
                
                for (int j = table.Rows.Count - 1; j >= 0; j--)
                {
                    if (table.Rows[j][1] == "")
                    {
                        table.Rows[j].Delete();
                    }

                }

                bool b = false;
                bool a = false;

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    b = false;
                    for (int k = 1; k < 5; k++)
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
                        MessageBox.Show("Block in row : " + (j + 1) + " and column : 4 should be a Number!!!");
                        break;
                    }
                    if (!Regex.Match(table.Rows[j][4].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                    {
                        a = true;
                        MessageBox.Show("Block in row : " + (j + 1) + " and column : 5 should be a Number!!!");
                        break;
                    }
                    if (!Regex.Match(table.Rows[j][5].ToString(), @"^[0-9]", RegexOptions.IgnoreCase).Success)
                    {
                        a = true;
                        MessageBox.Show("Block in row : " + (j + 1) + " and column : 6 should be a Number!!!");
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

                    SqlCommand del = new SqlCommand("delete from food", manager);
                    del.ExecuteNonQuery();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string c = "insert into food values('" + (i + 1) + "','" + table.Rows[i][1] + "','" + table.Rows[i][2] + "','" + table.Rows[i][3] + "','" + table.Rows[i][4] + "','" + table.Rows[i][5] + "','" + table.Rows[i][6] + "','" + table.Rows[i][7] +"','"+table.Rows[i][8]+ "')";
                        SqlCommand command = new SqlCommand(c, manager);
                        command.ExecuteNonQuery();
                    }

                    r = table.Rows.Count.ToString();

                }
                SqlDataAdapter adapter1 = new SqlDataAdapter("select * from food", manager);
                DataTable foods = new DataTable();
                adapter1.Fill(foods);
                foodtable.DataContext = foods;
                manager.Close();
            }
            catch
            {

            }
        }

        private void foodtable_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void foodtable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlConnection manager = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DtatBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            manager.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from food", manager);
            DataTable table = new DataTable();
            adapter.Fill(table);
            string r = foodtable.SelectedIndex.ToString();
            try
            {
                it.Text = table.Rows[int.Parse(r)][6].ToString();
                
            }
            catch
            {

            }
            manager.Close();
            try
            {
                fi.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(it.Text.Trim());
            }
            catch
            {

            }
            


        }

        private void ci_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (System.Windows.Forms.DialogResult.OK.ToString() == "OK")
            {
                it.Text = dialog.FileName.ToString();
                fi.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(it.Text.Trim());
            }

        }

        private void allorders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table = new DataTable();
            table = ((DataView)allorders.ItemsSource).ToTable();
            int r =int.Parse(allorders.SelectedIndex.ToString());
            cos.Text = table.Rows[r][1].ToString();
            adr.Text = table.Rows[r][2].ToString();
            string o = table.Rows[r][3].ToString();
            string[] x = o.Split(',');
            string fact = "";
            for (int i = 0; i < x.Length; i++)
            {
                string[] y = x[i].Split('-');
                string[] z = y[2].Split(' ');
                fact += (i + 1).ToString() + "-" + y[0] + " : " + y[1] + " at " + z[0] + "\n";
            }
            string incom = table.Rows[r][5].ToString()+"\n";
            string outcom= table.Rows[r][4].ToString()+"\n";
            fact += "Income : " + incom;
            fact += "Outcome : " + outcom;
            factor.Text = fact;
            if (table.Rows[r][6].ToString() == "yes")
            {
                pay.Text = "Paid!!!";

                pay.Background = Brushes.Green;
            }
            else
            {
                pay.Text = "Not Paid Yet!!!";
                pay.Background = Brushes.Red;
            }


        }

    }
}
