﻿using System;
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
    }
}
