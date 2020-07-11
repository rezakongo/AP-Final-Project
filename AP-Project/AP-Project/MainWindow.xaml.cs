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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AP_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            Button btn = new Button();
            
            InitializeComponent();
        }

        private void manager(object sender, RoutedEventArgs e)
        {
            manager_login mg = new manager_login();
            this.Content = mg;
        }

        private void client(object sender, RoutedEventArgs e)
        {
            client_login cl = new client_login();
            this.Content = cl;
        }
    }
}
