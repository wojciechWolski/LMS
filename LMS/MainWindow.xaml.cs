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
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Security.Cryptography;

namespace LMS
{
    
    public partial class MainWindow : Window
    {
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var pass = GetHashString(boxPassword.Password);
            string connectionString = @"Data Source=localhost;Initial Catalog=library;Integrated Security=True";
            using (LibdbContext db = new LibdbContext(connectionString))
            {
                var query = from u in db.Admins where u.Username == tboxUsername.Text && u.Password == pass select u;
                if (query.Count() > 0)
                {
                    this.Hide();
                    Panel pnl = new Panel();
                    pnl.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username/password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }


        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Signup sgn = new Signup();
            sgn.Show();
        }

    }
}
