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
    }
}
