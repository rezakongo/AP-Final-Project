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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AP_Project
{
    /// <summary>
    /// Interaction logic for manager_login.xaml
    /// </summary>
    public partial class manager_login : Page
    {
        public manager_login()
        {
            InitializeComponent();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Content = mw;
        }
    }
}
