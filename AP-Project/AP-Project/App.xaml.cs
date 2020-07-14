using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AP_Project
{
    public class client
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int nationalID { get; set; }
        public int phonenumber { get; set; }
        public string adress { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

    }
}
